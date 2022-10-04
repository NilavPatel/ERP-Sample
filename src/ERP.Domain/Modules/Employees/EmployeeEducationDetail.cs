using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Exceptions;

namespace ERP.Domain.Modules.Employees
{
    public class EmployeeEducationDetail : BaseAuditableEntity
    {
        public EmployeeEducationDetail()
        { }

        public EmployeeEducationDetail(Guid id,
            Guid employeeId,
            string degree,
            string instituteName,
            int passingMonth,
            int passingYear,
            int percentage,
            Guid createdBy)
        {
            Id = id;
            EmployeeId = employeeId;
            Degree = degree;
            InstituteName = instituteName;
            PassingMonth = passingMonth;
            PassingYear = passingYear;
            Percentage = percentage;
            CreatedOn = DateTimeOffset.Now;
            CreatedBy = createdBy;
        }

        public static EmployeeEducationDetail Create(Guid id,
            Guid employeeId,
            string degree,
            string instituteName,
            int passingMonth,
            int passingYear,
            int percentage,
            Guid createdBy,
            Func<Guid, Task<bool>> isEmployeeExist)
        {
            Guard.Against.Null(id, "Id");
            Guard.Against.Null(employeeId, "Employee Id");
            Guard.Against.NullOrWhiteSpace(degree, "Degree");
            Guard.Against.NullOrWhiteSpace(instituteName, "Institute Name");
            Guard.Against.MaximumLength(degree, "Degree", 200);
            Guard.Against.MaximumLength(instituteName, "Institute Name", 200);
            Guard.Against.Null(passingMonth, "passingMonth");
            Guard.Against.Null(passingYear, "passingYear");
            Guard.Against.Null(percentage, "Percentage");
            Guard.Against.Null(createdBy, "CreatedBy");

            var isValid = isEmployeeExist(employeeId).ConfigureAwait(false).GetAwaiter().GetResult();
            if (!isValid)
            {
                throw new DomainException("Employee Not Exist.");
            }

            return new EmployeeEducationDetail(id, employeeId, degree, instituteName, passingMonth,
                passingYear, percentage, createdBy);
        }

        public void Update(
            string degree,
            string instituteName,
            int passingMonth,
            int passingYear,
            int percentage,
            Guid modifiedBy)
        {
            Guard.Against.NullOrWhiteSpace(degree, "Degree");
            Guard.Against.NullOrWhiteSpace(instituteName, "Institute Name");
            Guard.Against.MaximumLength(degree, "Degree", 200);
            Guard.Against.MaximumLength(instituteName, "Institute Name", 200);
            Guard.Against.Null(passingMonth, "passingMonth");
            Guard.Against.Null(passingYear, "passingYear");
            Guard.Against.Null(percentage, "Percentage");
            Guard.Against.Null(modifiedBy, "Modified By");

            Degree = degree;
            InstituteName = instituteName;
            PassingMonth = passingMonth;
            PassingYear = passingYear;
            Percentage = percentage;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void Remove(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            IsDeleted = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Degree { get; set; }
        public string InstituteName { get; set; }
        public int PassingMonth { get; set; }
        public int PassingYear { get; set; }
        public int Percentage { get; set; }

        public Employee Employee { get; protected set; }
    }
}