using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees
{
    public class GetEmployeeDocumentsReq : IRequest<IList<EmployeeDocument>>
    {
        public Guid EmployeeId { get; set; }
    }

    public class DownloadEmployeeDocumentReq : IRequest<EmployeeDocument>
    {
        public Guid DocumentId { get; set; }
    }

}