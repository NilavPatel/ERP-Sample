using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeeDocumentSpecifications
    {
        public static BaseSpecification<EmployeeDocument> GetEmployeeDocumentByEmployeeIdSpec(Guid employeeId)
        {
            var sepc = new BaseSpecification<EmployeeDocument>(x => x.EmployeeId == employeeId
                && x.Employee.IsDeleted == false);
            sepc.ApplyOrderByDescending(x => x.CreatedOn);
            return sepc;
        }

        public static BaseSpecification<EmployeeDocument> GetEmployeeDocumentByIdSpec(Guid id)
        {
            return new BaseSpecification<EmployeeDocument>(x => x.Id == id
                 && x.Employee.IsDeleted == false);
        }
    }
}