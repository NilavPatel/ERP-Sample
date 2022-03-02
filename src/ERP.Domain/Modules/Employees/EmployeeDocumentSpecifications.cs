using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeeDocumentSpecifications
    {
        public static BaseSpecification<EmployeeDocument> GetEmployeeDocumentByEmployeeId(Guid employeeId)
        {
            var sepc = new BaseSpecification<EmployeeDocument>(x => x.EmployeeId == employeeId
                && x.IsDeleted == false
                && x.Employee.IsDeleted == false);
            return sepc;
        }

        public static BaseSpecification<EmployeeDocument> GetEmployeeDocumentById(Guid id)
        {
            var spec = new BaseSpecification<EmployeeDocument>(x => x.Id == id
                 && x.IsDeleted == false
                 && x.Employee.IsDeleted == false);
            return spec;
        }
    }
}