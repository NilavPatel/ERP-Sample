using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeePersonalDetailSpecifications
    {
        public static BaseSpecification<EmployeePersonalDetail> GetPersonalDetailByIdSpec(Guid employeeId)
        {
            var spec = new BaseSpecification<EmployeePersonalDetail>(x => x.EmployeeId == employeeId);
            spec.AddInclude(x => x.Employee);
            return spec;
        }
    }
}