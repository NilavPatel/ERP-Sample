using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeeBankDetailSpecifications
    {
        public static BaseSpecification<EmployeeBankDetail> GetBankDetailByEmployeeId(Guid employeeId)
        {
            var spec = new BaseSpecification<EmployeeBankDetail>(x => x.EmployeeId == employeeId
                 && x.IsDeleted == false
                 && x.Employee.IsDeleted == false);
            return spec;
        }
    }
}