using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Roles
{
    public class RoleSpecifications
    {
        public static BaseSpecification<Role> GetRoleByIdSpec(Guid id)
        {
            return new BaseSpecification<Role>(x => x.Id == id && x.IsDeleted == false);
        }

        public static BaseSpecification<Role> GetAllRolesSpec()
        {
            return new BaseSpecification<Role>(x => x.IsDeleted == false);
        }

        public static BaseSpecification<Role> SearchRolesSpec(string searchKeyword)
        {
            return new BaseSpecification<Role>(x => (x.Name.Contains(searchKeyword)
                    || x.Description.Contains(searchKeyword))
                && x.IsDeleted == false);
        }

        public static BaseSpecification<Role> GetByRoleNameSpec(string name)
        {
            return new BaseSpecification<Role>(x => x.Name == name 
                && x.IsDeleted == false);
        }

    }
}