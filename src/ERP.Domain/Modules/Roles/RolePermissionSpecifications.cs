using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Roles
{
    public class RolePermissionSpecifications
    {
        public static BaseSpecification<RolePermission> GetByRoleIdSpec(Guid roleId)
        {
            return new BaseSpecification<RolePermission>(x => x.RoleId == roleId);
        }

        public static BaseSpecification<RolePermission> GetByRoleIdsSpec(IEnumerable<Guid> roleIds)
        {
            return new BaseSpecification<RolePermission>(x => roleIds.Contains(x.RoleId));
        }

    }
}