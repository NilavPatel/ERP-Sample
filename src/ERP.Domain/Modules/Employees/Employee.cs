using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Departments;
using ERP.Domain.Modules.Designations;
using ERP.Domain.Modules.Users;

namespace ERP.Domain.Modules.Employees
{
    public class Employee : AggregateRoot
    {
        #region Constructor

        public Employee()
        { }

        private Employee(Guid id,
            string firstName,
            string middleName,
            string lastName,
            string employeeCode,
            DateTimeOffset joiningOn,
            Guid createdBy)
        {

            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            EmployeeCode = employeeCode;
            JoiningOn = joiningOn;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }
        #endregion

        #region Behaviours

        public static Employee Create(
            string firstName,
            string middleName,
            string lastName,
            string employeeCode,
            DateTimeOffset joiningOn,
            Guid createdBy,
            Func<string, Task<bool>> isEmployeeCodeExist
        )
        {
            Guard.Against.NullOrWhiteSpace(firstName, "First Name");
            Guard.Against.MaximumLength(firstName, "First Name", 20);
            Guard.Against.NullOrWhiteSpace(middleName, "Middle Name");
            Guard.Against.MaximumLength(middleName, "Middle Name", 20);
            Guard.Against.NullOrWhiteSpace(lastName, "Last Name");
            Guard.Against.MaximumLength(lastName, "Last Name", 20);
            Guard.Against.NullOrWhiteSpace(employeeCode, "Employee Code");
            Guard.Against.Null(joiningOn, "Joining On");
            Guard.Against.Null(createdBy, "Created By");

            var employeeCodeExist = isEmployeeCodeExist(employeeCode).ConfigureAwait(false).GetAwaiter().GetResult();
            if (employeeCodeExist)
            {
                throw new DomainException("Employee Code is already exist.");
            }

            return new Employee(Guid.NewGuid(), firstName, middleName, lastName, employeeCode, joiningOn, createdBy);
        }

        public void UpdateEmployee(
            string firstName,
            string middleName,
            string lastName,
            string? officeEmailId,
            string? officeContactNo,
            DateTimeOffset joiningOn,
            DateTimeOffset? confirmationOn,
            DateTimeOffset? resignationOn,
            DateTimeOffset? relievingOn,
            Guid? designationId,
            Guid? reportingToId,
            Guid? departmentId,
            Guid modifiedBy,
            Func<Guid, Task<bool>> isDesignationExist,
            Func<Guid, Task<bool>> isReportingToExist,
            Func<Guid, Task<bool>> isDepartmentExist)
        {

            Guard.Against.NullOrWhiteSpace(firstName, "First Name");
            Guard.Against.MaximumLength(firstName, "First Name", 20);
            Guard.Against.NullOrWhiteSpace(middleName, "Middle Name");
            Guard.Against.MaximumLength(middleName, "Middle Name", 20);
            Guard.Against.NullOrWhiteSpace(lastName, "Last Name");
            Guard.Against.MaximumLength(lastName, "Last Name", 20);
            if (!string.IsNullOrWhiteSpace(officeEmailId))
            {
                Guard.Against.InValidEmailId(officeEmailId, "Official Email Id");
                Guard.Against.MaximumLength(officeEmailId, "Official Email Id", 50);
            }
            Guard.Against.Alphabet(officeContactNo ?? string.Empty, "Official Mobile No");
            Guard.Against.MaximumLength(officeContactNo ?? string.Empty, "Official Mobile No", 15);
            Guard.Against.Null(joiningOn, "Joining On");

            if (confirmationOn.HasValue)
            {
                Guard.Against.DateTimeOffsetLessThanOrEqual(confirmationOn.Value, "Confirmation On", joiningOn);
            }
            if (resignationOn.HasValue)
            {
                Guard.Against.DateTimeOffsetLessThanOrEqual(resignationOn.Value, "Resignation On", joiningOn);
                if (confirmationOn.HasValue)
                {
                    Guard.Against.DateTimeOffsetLessThanOrEqual(resignationOn.Value, "Resignation On", confirmationOn.Value);
                }
            }
            if (relievingOn.HasValue)
            {
                Guard.Against.DateTimeOffsetLessThanOrEqual(relievingOn.Value, "Relieving On", joiningOn);
                if (confirmationOn.HasValue)
                {
                    Guard.Against.DateTimeOffsetLessThanOrEqual(relievingOn.Value, "Relieving On", confirmationOn.Value);
                }
                if (resignationOn.HasValue)
                {
                    Guard.Against.DateTimeOffsetLessThanOrEqual(relievingOn.Value, "Relieving On", resignationOn.Value);
                }
            }

            Guard.Against.Null(modifiedBy, "Modified By");

            if (designationId.HasValue)
            {
                var designationExist = isDesignationExist(designationId.Value).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!designationExist)
                {
                    throw new RecordNotFoundException("Designation Not Found");
                }
            }

            if (reportingToId.HasValue)
            {
                var reportingToExist = isReportingToExist(reportingToId.Value).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!reportingToExist)
                {
                    throw new RecordNotFoundException("Reporting To Not Found");
                }
            }

            if (departmentId.HasValue)
            {
                var departmentExist = isDepartmentExist(departmentId.Value).ConfigureAwait(false).GetAwaiter().GetResult();
                if (!departmentExist)
                {
                    throw new RecordNotFoundException("Department Not Found");
                }
            }

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            OfficeEmailId = officeEmailId;
            OfficeContactNo = officeContactNo;
            JoiningOn = joiningOn;
            ConfirmationOn = confirmationOn;
            ResignationOn = resignationOn;
            RelievingOn = relievingOn;
            DesignationId = designationId;
            ReportingToId = reportingToId;
            DepartmentId = departmentId;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void UploadProfilePhoto(string? photoName, Guid? modifiedBy)
        {
            Guard.Against.MaximumLength(photoName ?? string.Empty, "Photo Name", 50);
            ProfilePhotoName = photoName;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        #endregion

        #region States
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string? ProfilePhotoName { get; set; }
        public string EmployeeCode { get; set; }
        public string? OfficeEmailId { get; set; }
        public string? OfficeContactNo { get; set; }
        public DateTimeOffset JoiningOn { get; set; }
        public DateTimeOffset? ConfirmationOn { get; set; }
        public DateTimeOffset? ResignationOn { get; set; }
        public DateTimeOffset? RelievingOn { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? ReportingToId { get; set; }
        public Guid? DepartmentId { get; set; }

        public EmployeePersonalDetail EmployeePersonalDetail { get; protected set; }
        public EmployeeBankDetail EmployeeBankDetail { get; protected set; }
        public User User { get; protected set; }
        public Designation Designation { get; protected set; }
        public Employee ReportingTo { get; protected set; }
        public Department Department { get; protected set; }

        public ICollection<EmployeeEducationDetail> EmployeeEducationDetails { get; protected set; }
        public ICollection<EmployeeDocument> EmployeeDocuments { get; protected set; }
        public ICollection<Employee> ReportingTos { get; protected set; }
        #endregion

        public string GetNameWithDesignation()
        {
            if (Designation != null)
            {
                return string.Format("{0} {1} ({2})", FirstName, LastName, Designation.Name);
            }
            return string.Format("{0} {1}", FirstName, LastName);
        }
    }
}