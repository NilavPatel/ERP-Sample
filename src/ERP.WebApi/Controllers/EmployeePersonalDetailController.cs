using ERP.Domain.Core.Services;
using ERP.Application.Modules.Employees.Commands;
using ERP.Domain.Enums;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ERP.Application.Modules.Employees.Queries;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeePersonalDetailController : BaseController
    {
        private readonly IUserContext _userContext;
        public EmployeePersonalDetailController(IMediator _mediator,
         IUserContext userContext) : base(_mediator)
        {
            _userContext = userContext;
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateEmployeePersonalDetail(UpdateEmployeePersonalDetailCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetEmployeePersonalDetailById(GetEmployeePersonalDetailByIdReq req)
        {
            var result = await _mediator.Send<EmployeePersonalDetailViewModel>(req);
            return new CustomActionResult(true, null, null, result);
        }

    }
}