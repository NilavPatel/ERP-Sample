using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Roles
{
    public class RolePermissionSpecifications
    {
        public static BaseSpecification<RolePermission> GetByRoleIdSpec(Guid roleId)
        {
            return new BaseSpecification<RolePermission>(x => x.RoleId == roleId
                 && x.Role.IsDeleted == false);
        }

    }
}