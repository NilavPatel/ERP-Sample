using ERP.Domain.Core.Services;
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

        public Guid GetCurrentEmployeeId()
        {
            return _userContext.GetCurrentEmployeeId();
        }
    }
}