using System.Security.Claims;

namespace ERP.Application.Core.Services
{
    public interface IUserContext
    {
        public ClaimsPrincipal User { get; }

        public Guid GetUserId();
    }
}