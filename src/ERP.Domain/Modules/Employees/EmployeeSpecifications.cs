using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeeSpecifications
    {
        public static BaseSpecification<Employee> GetEmployeeByIdSpec(Guid id)
        {
            var spec = new BaseSpecification<Employee>(x => x.Id == id && x.IsDeleted == false);
            return spec;
        }

        public static BaseSpecification<Employee> GetEmployeeByEmployeeCodeSpec(string employeeCode)
        {
            return new BaseSpecification<Employee>(x => x.EmployeeCode == employeeCode
                && x.IsDeleted == false);
        }

        public static BaseSpecification<Employee> GetAllEmployeesSpec()
        {
            var spec = new BaseSpecification<Employee>(x => x.IsDeleted == false);
            spec.ApplyOrderBy(x => x.EmployeeCode);
            return spec;
        }

        public static BaseSpecification<Employee> GetAllEmployeesExceptIdSpec(Guid id, string searchKeyword)
        {
            var spec = new BaseSpecification<Employee>(x => (x.EmployeeCode.Contains(searchKeyword)
                     || x.FirstName.Contains(searchKeyword)
                     || x.LastName.Contains(searchKeyword)
                     || x.MiddleName.Contains(searchKeyword)
                     || x.OfficeEmailId.Contains(searchKeyword)
                     || x.OfficeContactNo.Contains(searchKeyword))
                    && x.Id != id
                    && x.IsDeleted == false);
            spec.ApplyOrderBy(x => x.EmployeeCode);
            return spec;
        }

        public static BaseSpecification<Employee> SearchEmployeesSpec(string searchKeyword)
        {
            var spec = new BaseSpecification<Employee>(x => (x.EmployeeCode.Contains(searchKeyword)
                     || x.FirstName.Contains(searchKeyword)
                     || x.LastName.Contains(searchKeyword)
                     || x.MiddleName.Contains(searchKeyword)
                     || x.OfficeEmailId.Contains(searchKeyword)
                     || x.OfficeContactNo.Contains(searchKeyword))
                 && x.IsDeleted == false);
            spec.ApplyOrderBy(x => x.EmployeeCode);
            return spec;
        }
    }
}