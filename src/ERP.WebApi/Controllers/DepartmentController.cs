using ERP.Application.Modules.Departments.Commands;
using ERP.Application.Modules.Departments.Queries;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Departments;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class DepartmentController : BaseController
    {
        public DepartmentController(IMediator _mediator) : base(_mediator)
        { }

        [CustomRoleAuthorizeFilter(PermissionEnum.DepartmentView)]
        [HttpPost]
        public async Task<CustomActionResult> GetAllDepartments(GetAllDepartmentsReq req)
        {
            var result = await _mediator.Send<GetAllDepartmentsRes>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DepartmentView)]
        [HttpPost]
        public async Task<CustomActionResult> GetDepartmentById(GetDepartmentByIdReq req)
        {
            var result = await _mediator.Send<Department>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DepartmentAdd)]
        [HttpPost]
        public async Task<CustomActionResult> CreateDepartment(CreateDepartmentCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DepartmentEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateDepartment(UpdateDepartmentCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.DepartmentDelete)]
        [HttpPost]
        public async Task<CustomActionResult> DeleteDepartment(DeleteDepartmentCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record removed sucessfully." }, null, result);
        }
    }
}