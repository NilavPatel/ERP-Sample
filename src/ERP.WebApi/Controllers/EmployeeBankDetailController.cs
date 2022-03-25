using ERP.Application.Modules.Employees.Commands;
using ERP.Application.Modules.Employees.Queries;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Employees;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeBankDetailController : BaseController
    {
        public EmployeeBankDetailController(IMediator _mediator) : base(_mediator)
        { }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> AddEmployeeBankDetail(AddEmployeeBankDetailCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateEmployeeBankDetail(UpdateEmployeeBankDetailCommand req)
        {
            var id = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, id);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.EmployeeView)]
        [HttpPost]
        public async Task<CustomActionResult> GetEmployeeBankDetail(GetEmployeeBankDetailReq req)
        {
            var result = await _mediator.Send<EmployeeBankDetail>(req);
            return new CustomActionResult(true, null, null, result);
        }
    }
}