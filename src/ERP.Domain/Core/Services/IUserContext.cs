using System.Security.Claims;

namespace ERP.Domain.Core.Services
{
    public interface IUserContext
    {
        public ClaimsPrincipal User { get; }

        public Guid GetCurrentEmployeeId();
    }
}