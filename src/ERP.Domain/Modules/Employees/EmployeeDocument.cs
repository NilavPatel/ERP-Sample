using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;

namespace ERP.Domain.Modules.Employees
{
    public class EmployeeDocument : BaseAuditableEntity
    {
        public EmployeeDocument()
        { }

        private EmployeeDocument(Guid id, Guid employeeId, string fileName, string? description, Guid createdBy)
        {
            Id = id;
            EmployeeId = employeeId;
            FileName = fileName;
            Description = description;
            CreatedBy = createdBy;
            CreatedOn = DateTime.UtcNow;
        }

        public static EmployeeDocument UploadDocument(Guid id, Guid employeeId, string fileName,
            string? description, Guid createdBy)
        {
            Guard.Against.Null(id, "Document Id");
            Guard.Against.Null(employeeId, "Employee Id");
            Guard.Against.NullOrWhiteSpace(fileName, "FileName");
            Guard.Against.MaximumLength(fileName, "FileName", 50);
            Guard.Against.MaximumLength(description ?? string.Empty, "Description", 200);
            Guard.Against.Null(createdBy, "Created By");

            return new EmployeeDocument(id, employeeId, fileName, description, createdBy);
        }

        public void RemoveDocument(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            IsDeleted = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string FileName { get; set; }
        public string? Description { get; set; }

        public virtual Employee Employee { get; protected set; }
    }
}