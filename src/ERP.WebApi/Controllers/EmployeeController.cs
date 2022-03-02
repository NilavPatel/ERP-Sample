using ERP.Application.Modules.Employees;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Employees;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class EmployeeController : BaseController
    {
        public EmployeeController(IMediator _mediator) : base(_mediator)
        { }

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
            var result = await _mediator.Send<IList<EmployeeViewModel>>(req);
            return new CustomActionResult(true, null, null, result);
        }
    }
}