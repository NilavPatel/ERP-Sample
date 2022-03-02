using ERP.Application.Core.Services;
using ERP.Application.Modules.Employees;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ERP.Domain.Modules.Employees;
using ERP.Domain.Enums;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
            var result = await _mediator.Send<IList<EmployeeDocument>>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> DownloadEmployeeDocument(Guid id, string token)
        {
            var claims = GetClaimsFromJwt(token);
            if (claims.Any())
            {
                var req = new DownloadEmployeeDocumentReq
                {
                    DocumentId = id,
                };
                var document = await _mediator.Send<EmployeeDocument>(req);
                var fileName = document.Id + Path.GetExtension(document.FileName);
                var result = await _fileService.DownloadFile(fileName);
                return File(result, "text/plain", Path.GetFileName(document.FileName));
            }
            return Unauthorized();
        }

        private IEnumerable<Claim> GetClaimsFromJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration.GetValue<string>("JWTSecretKey");
            if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 24)
            {
                throw new ArgumentException("JWT secret key not available.");
            }
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            return jwtToken.Claims;
        }
    }
}