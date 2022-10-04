using ERP.Domain.Core.Specifications;

namespace ERP.Domain.Modules.Employees
{
    public static class EmployeeDocumentSpecifications
    {
        public static BaseSpecification<EmployeeDocument> GetEmployeeDocumentByEmployeeIdSpec(Guid employeeId)
        {
            var spec = new BaseSpecification<EmployeeDocument>(x => x.EmployeeId == employeeId);
            spec.AddInclude(x => x.Employee);
            spec.ApplyOrderByDescending(x => x.CreatedOn);
            return spec;
        }

        public static BaseSpecification<EmployeeDocument> GetEmployeeDocumentByIdSpec(Guid id)
        {
            var spec = new BaseSpecification<EmployeeDocument>(x => x.Id == id);
            spec.AddInclude(x => x.Employee);
            return spec;
        }
    }
}