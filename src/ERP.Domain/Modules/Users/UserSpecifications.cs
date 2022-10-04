using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Users
{
    public static class UserSpecifications
    {
        public static BaseSpecification<User> GetAllUsersSpec()
        {
            var spec = new BaseSpecification<User>();
            spec.AddInclude(x => x.Employee);
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<User> GetUserByIdSpec(Guid id)
        {
            var spec = new BaseSpecification<User>(x => x.Id == id);
            spec.AddInclude(x => x.Employee);
            return spec;
        }

        public static BaseSpecification<User> GetUserByEmployeeIdSpec(Guid id)
        {
            var spec = new BaseSpecification<User>(x => x.EmployeeId == id);
            spec.AddInclude(x => x.Employee);
            return spec;
        }

        public static BaseSpecification<User> GetUserByEmployeeCodeSpec(string employeeCode)
        {
            var spec = new BaseSpecification<User>(x => x.Employee.EmployeeCode == employeeCode);
            spec.AddInclude(x => x.Employee);
            return spec;
        }

        public static BaseSpecification<User> SearchUsersSpec(string searchKeyword)
        {
            var spec = new BaseSpecification<User>(x =>
                (x.Employee.EmployeeCode.Contains(searchKeyword)
                    || x.Employee.FirstName.Contains(searchKeyword)
                    || x.Employee.LastName.Contains(searchKeyword)
                    || x.Employee.MiddleName.Contains(searchKeyword)
                    || (x.Employee.OfficeEmailId != null && x.Employee.OfficeEmailId.Contains(searchKeyword))
                    || (x.Employee.OfficeContactNo != null && x.Employee.OfficeContactNo.Contains(searchKeyword))));
            spec.AddInclude(x => x.Employee);
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }
    }
}