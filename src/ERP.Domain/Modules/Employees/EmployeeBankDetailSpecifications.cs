using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeeBankDetailSpecifications
    {
        public static BaseSpecification<EmployeeBankDetail> GetBankDetailByEmployeeIdSpec(Guid employeeId)
        {
            return new BaseSpecification<EmployeeBankDetail>(x => x.EmployeeId == employeeId
                 && x.Employee.IsDeleted == false);
        }
    }
}