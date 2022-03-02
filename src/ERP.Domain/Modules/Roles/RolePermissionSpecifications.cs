using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Roles
{
    public class RolePermissionSpecifications
    {
        public static BaseSpecification<RolePermission> GetByRoleIdSpec(Guid roleId)
        {
            var spec = new BaseSpecification<RolePermission>(x => x.RoleId == roleId
                 && x.IsDeleted == false
                 && x.Role.IsDeleted == false);
            return spec;
        }

    }
}