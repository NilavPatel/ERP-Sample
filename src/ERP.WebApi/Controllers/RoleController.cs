using ERP.Application.Modules.Roles;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Roles;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class RoleController : BaseController
    {
        public RoleController(IMediator _mediator) : base(_mediator)
        { }

        [CustomRoleAuthorizeFilter(PermissionEnum.RoleView)]
        [HttpPost]
        public async Task<CustomActionResult> GetAllRoles(GetAllRolesReq req)
        {
            var result = await _mediator.Send<GetAllRolesRes>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.RoleView)]
        [HttpPost]
        public async Task<CustomActionResult> GetRoleById(GetRoleById req)
        {
            var result = await _mediator.Send<Role>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.RoleAdd)]
        [HttpPost]
        public async Task<CustomActionResult> CreateRole(CreateRole req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.RoleEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateRole(UpdateRole req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }
    }
}