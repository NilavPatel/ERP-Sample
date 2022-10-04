using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeeBankDetailSpecifications
    {
        public static BaseSpecification<EmployeeBankDetail> GetBankDetailByEmployeeIdSpec(Guid employeeId)
        {
            var spec = new BaseSpecification<EmployeeBankDetail>(x => x.EmployeeId == employeeId);
            spec.AddInclude(x => x.Employee);
            return spec;
        }
    }
}