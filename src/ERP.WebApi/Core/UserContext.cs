using System.Security.Claims;
using ERP.Application.Core.Services;

namespace ERP.WebApi.Core
{
    public class UserContext : IUserContext
    {
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            User = httpContextAccessor.HttpContext.User;
        }

        public ClaimsPrincipal User { get; }

        public Guid GetUserId()
        {
            return Guid.Parse(User.Claims.First(x => x.Type == "nameid").Value);
        }
    }
}