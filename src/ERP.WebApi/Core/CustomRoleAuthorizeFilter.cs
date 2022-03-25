using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ERP.WebApi.Core
{
    public class CustomRoleAuthorizeFilter : Attribute, IAuthorizationFilter
    {
        private readonly PermissionEnum _permission;
        IUserContext _userContext;
        IUnitOfWork _unitOfWork;
        public CustomRoleAuthorizeFilter(PermissionEnum permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _userContext = context.HttpContext.RequestServices.GetService<IUserContext>();
            _unitOfWork = context.HttpContext.RequestServices.GetService<IUnitOfWork>();
            if (_userContext == null || _unitOfWork == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var employeeId = _userContext.GetCurrentEmployeeId();
            var spec = UserSpecifications.GetUserByEmployeeIdSpec(employeeId);
            spec.AddInclude(x => x.Role);
            spec.AddInclude(x => x.Role.RolePermissions);
            var user = _unitOfWork.Repository<User>().SingleAsync(spec, false).ConfigureAwait(false).GetAwaiter().GetResult();

            if (user.Role == null
                || !user.Role.RolePermissions.Any(x => x.PermissionId == (int)_permission))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}