using MediatR;
using Microsoft.AspNetCore.Http;

namespace ERP.Application.Modules.Employees.Commands
{
    public class UploadEmployeeDocumentCommand : IRequest<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string? Description { get; set; }
        public IFormFile Document { get; set; }
    }

    public class RemoveEmployeeDocumentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
    }
}