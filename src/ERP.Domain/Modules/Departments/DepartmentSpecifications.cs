using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Departments
{
    public class DepartmentSpecifications
    {
        public static BaseSpecification<Department> GetDepartmentByIdSpec(Guid id)
        {
            return new BaseSpecification<Department>(x => x.Id == id);
        }

        public static BaseSpecification<Department> GetAllDepartmentsSpec()
        {
            var spec = new BaseSpecification<Department>();
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<Department> SearchDepartmentsSpec(string searchKeyword)
        {
            var spec = new BaseSpecification<Department>(x => x.Name.Contains(searchKeyword)
                    || x.Description.Contains(searchKeyword));
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<Department> GetByDepartmentNameSpec(string name)
        {
            return new BaseSpecification<Department>(x => x.Name == name);
        }

    }
}