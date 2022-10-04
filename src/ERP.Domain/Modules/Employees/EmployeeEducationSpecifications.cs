using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeeEducationSpecifications
    {
        public static BaseSpecification<EmployeeEducationDetail> GetEducationByEmployeeIdSpec(Guid employeeId)
        {
            var sepc = new BaseSpecification<EmployeeEducationDetail>(x => x.EmployeeId == employeeId);
            sepc.ApplyOrderByDescending(x => x.CreatedOn);
            return sepc;
        }

        public static BaseSpecification<EmployeeEducationDetail> GetEmployeeEducationByIdSpec(Guid id)
        {
            var spec = new BaseSpecification<EmployeeEducationDetail>(x => x.Id == id);
            return spec;
        }
    }
}