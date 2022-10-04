using ERP.Domain.Core.Services;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Domain.Modules.Employees;
using ERP.Domain.Enums;
using ERP.Application.Modules.Employees.Queries;
using ERP.Application.Modules.Employees.Commands;
using ERP.Application.Core.Helpers;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeDocumentController : BaseController
    {
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        public EmployeeDocumentController(IMediator _mediator,
         IFileService fileService,
         IConfiguration configuration) : base(_mediator)
        {
            _fileService = fileService;
            _configuration = configuration;
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UploadEmployeeDocument([FromForm] UploadEmployeeDocumentCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> RemoveEmployeeDocument(RemoveEmployeeDocumentCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record removed sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetEmployeeDocuments(GetEmployeeDocumentsReq req)
        {
            var result = await _mediator.Send<IList<EmployeeDocumentViewModel>>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadEmployeeDocument(Guid id, string token)
        {
            var secretKey = _configuration.GetValue<string>("JWTSecretKey");
            var claims = JWTHelper.ValidateTokenWithLifeTime(token, secretKey);
            if (claims.Any())
            {
                var req = new DownloadEmployeeDocumentReq
                {
                    DocumentId = id,
                };
                var document = await _mediator.Send<EmployeeDocumentViewModel>(req);
                var fileName = document.Id + Path.GetExtension(document.FileName);
                var result = await _fileService.DownloadFile(fileName);
                return File(result, "text/plain", Path.GetFileName(document.FileName));
            }
            return Unauthorized();
        }
    }
}