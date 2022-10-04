using ERP.Domain.Core.Services;
using ERP.Application.Modules.Employees.Commands;
using ERP.Application.Modules.Employees.Queries;
using ERP.Domain.Enums;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ERP.Application.Core.Helpers;
using ERP.Application.Core.Models;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : BaseController
    {
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        private readonly IUserContext _userContext;
        public EmployeeController(IMediator _mediator,
         IFileService fileService,
         IConfiguration configuration,
         IUserContext userContext) : base(_mediator)
        {
            _fileService = fileService;
            _configuration = configuration;
            _userContext = userContext;
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeAdd)]
        [HttpPost]
        public async Task<CustomActionResult> CreateEmployee(CreateEmployeeCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateEmployee(UpdateEmployeeCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetAllEmployees(GetAllEmployeesReq req)
        {
            var result = await _mediator.Send<GetAllEmployeesRes>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetEmployeeById(GetEmployeeByIdReq req)
        {
            var result = await _mediator.Send<EmployeeViewModel>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetAvailableReportingPersons(GetAvailableReportingPersonsReq req)
        {
            var result = await _mediator.Send<IList<SelectListItem>>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UploadEmployeeProfilePhoto([FromForm] UploadEmployeeProfilePhotoCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> RemoveEmployeeProfilePhoto(RemoveEmployeeProfilePhotoCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [HttpPost]
        public async Task<CustomActionResult> GetLoginEmployeeDetails()
        {
            var id = _userContext.GetCurrentEmployeeId();
            var req = new GetEmployeeByIdReq
            {
                Id = id
            };
            var result = await _mediator.Send<EmployeeViewModel>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> GetEmployeeProfilePhoto(string photoName, string token)
        {
            var secretKey = _configuration.GetValue<string>("JWTSecretKey");
            var claims = JWTHelper.ValidateTokenWithLifeTime(token, secretKey);
            if (claims.Any())
            {
                var result = await _fileService.DownloadFile(photoName);
                return File(result, "text/plain", Path.GetFileName(photoName));
            }
            return Unauthorized();
        }
    }
}