using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ERP.Domain.Enums;
using ERP.Application.Modules.Employees.Commands;
using ERP.Application.Modules.Employees.Queries;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeEducationController : BaseController
    {
        private readonly IConfiguration _configuration;
        public EmployeeEducationController(IMediator _mediator,
         IConfiguration configuration) : base(_mediator)
        {
            _configuration = configuration;
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> AddEmployeeEducation(AddEmployeeEducationCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateEmployeeEducation(UpdateEmployeeEducationCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record saved sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> RemoveEmployeeEducation(RemoveEmployeeEducationCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record removed sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetEmployeeEducations(GetEmployeeEducationsReq req)
        {
            var result = await _mediator.Send<IList<EmployeeEducationViewModel>>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeView)]
        [HttpPost]
        public async Task<IActionResult> GetEmployeeEducationById(GetEmployeeEducationByIdReq req)
        {
            var result = await _mediator.Send<EmployeeEducationViewModel>(req);
            return new CustomActionResult(true, null, null, result);
        }
    }
}