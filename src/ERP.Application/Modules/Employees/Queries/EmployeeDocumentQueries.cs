using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeeDocumentsReq : IRequest<IList<EmployeeDocumentViewModel>>
    {
        public Guid EmployeeId { get; set; }
    }

    public class DownloadEmployeeDocumentReq : IRequest<EmployeeDocumentViewModel>
    {
        public Guid DocumentId { get; set; }
    }

    public class EmployeeDocumentViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }
    }

}