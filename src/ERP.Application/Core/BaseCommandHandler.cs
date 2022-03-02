using System.Security.Claims;
using ERP.Application.Core.Services;
using ERP.Domain.Core.Models;
using MediatR;

namespace ERP.Application.Core
{
    public class BaseCommandHandler
    {
        private readonly IMediator _mediator;
        private readonly IUserContext _userContext;
        public BaseCommandHandler(IMediator mediator, IUserContext userContext)
        {
            _mediator = mediator;
            _userContext = userContext;
        }

        public Guid GetUserId()
        {
            return _userContext.GetUserId();
        }
    }
}