using ERP.Application.Modules.Dashboard.Queries;
using ERP.WebApi.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    public class DashboardController : BaseController
    {
        public DashboardController(IMediator _mediator) : base(_mediator)
        { }

        [HttpGet]
        public async Task<CustomActionResult> GetWeeklyBirthdays()
        {
            var result = await _mediator.Send<GetWeeklyBirthdaysRes>(new GetWeeklyBirthdaysReq());
            return new CustomActionResult(true, null, null, result);
        }
    }
}