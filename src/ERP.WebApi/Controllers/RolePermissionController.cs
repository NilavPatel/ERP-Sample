using ERP.Application.Modules.Roles.Commands;
using ERP.Application.Modules.Roles.Queries;
using ERP.Domain.Enums;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class RolePermissionController : BaseController
    {
        public RolePermissionController(IMediator _mediator) : base(_mediator)
        { }

        [CustomRoleAuthorizeFilter(PermissionEnum.RoleEdit)]
        [HttpPost]
        public async Task<CustomActionResult> AddRolePermissions(AddRolePermissionsCommnd req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.RoleView)]
        [HttpPost]
        public async Task<CustomActionResult> GetAllRolePermissionByRoleId(GetAllRolePermissionByRoleIdReq req)
        {
            var result = await _mediator.Send<IList<RolePermissionViewModel>>(req);
            return new CustomActionResult(true, null, null, result);
        }
    }
}