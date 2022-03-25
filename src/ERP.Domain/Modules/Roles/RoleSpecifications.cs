using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Roles
{
    public class RoleSpecifications
    {
        public static BaseSpecification<Role> GetRoleByIdSpec(Guid id)
        {
            return new BaseSpecification<Role>(x => x.Id == id);
        }

        public static BaseSpecification<Role> GetAllRolesSpec()
        {
            var spec = new BaseSpecification<Role>();
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<Role> SearchRolesSpec(string searchKeyword)
        {
            var spec = new BaseSpecification<Role>(x => (x.Name.Contains(searchKeyword)
                     || x.Description.Contains(searchKeyword))
                 );
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<Role> GetByRoleNameSpec(string name)
        {
            return new BaseSpecification<Role>(x => x.Name == name);
        }

    }
}