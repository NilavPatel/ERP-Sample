using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ERP.WebApi.Core
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public BaseController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}