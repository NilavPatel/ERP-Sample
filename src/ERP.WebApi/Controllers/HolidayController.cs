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
    public class HolidayController : BaseController
    {
        public HolidayController(IMediator _mediator) : base(_mediator)
        { }

        [HttpPost]
        public async Task<CustomActionResult> GetAllHolidays(GetAllHolidaysReq req)
        {
            var result = await _mediator.Send<GetAllHolidaysRes>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.HolidayView)]
        [HttpPost]
        public async Task<CustomActionResult> GetHolidayById(GetHolidayByIdReq req)
        {
            var result = await _mediator.Send<Holiday>(req);
            return new CustomActionResult(true, null, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.HolidayAdd)]
        [HttpPost]
        public async Task<CustomActionResult> CreateHoliday(CreateHolidayCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record created sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.HolidayEdit)]
        [HttpPost]
        public async Task<CustomActionResult> UpdateHoliday(UpdateHolidayCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record updated sucessfully." }, null, result);
        }

        [CustomRoleAuthorizeFilter(PermissionEnum.HolidayDelete)]
        [HttpPost]
        public async Task<CustomActionResult> DeleteHoliday(DeleteHolidayCommand req)
        {
            var result = await _mediator.Send<Guid>(req);
            return new CustomActionResult(true, new string[] { "Record removed sucessfully." }, null, result);
        }

        [HttpPost]
        public async Task<CustomActionResult> GetAllHolidayByYear(GetAllHolidayByYearReq req)
        {
            var result = await _mediator.Send<IList<Holiday>>(req);
            return new CustomActionResult(true, null, null, result);
        }
    }
}