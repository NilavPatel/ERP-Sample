using ERP.Application.Modules.Leaves.Commands;
using ERP.Application.Modules.Leaves.Queries;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Leaves;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class LeaveTypeController : BaseController
    {
        public LeaveTypeController(IMediator _mediator) : base(_mediator)
        { }

        [HttpPost]
        public async Task<CustomActionResult> GetAllLeaveTypes(GetAllLeaveTypesReq req)
        {
            var result = await _mediator.Send<GetAllLeaveTypesRes>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.LeaveTypeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetLeaveTypeById(GetLeaveTypeByIdReq req)
        {
            var result = await _mediator.Send<LeaveType>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.LeaveTypeAdd)]
        [HttpPost]
        public async Task<CustomActionResult> CreateLeaveType(CreateLeaveTypeCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.LeaveTypeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateLeaveType(UpdateLeaveTypeCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.LeaveTypeDelete)]
        [HttpPost]
        public async Task<CustomActionResult> DeleteLeaveType(DeleteLeaveTypeCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record removed sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.LeaveTypeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetAllActiveLeaveTypes(GetAllActiveLeaveTypesReq req)
        {
            var result = await _mediator.Send<IList<LeaveType>>(req);
            return new CustomActionResult(true, null, null, result);
        }
    }
}