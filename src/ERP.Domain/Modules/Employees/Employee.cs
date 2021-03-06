using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Enums;
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
            DateTimeOffset? birthDate,
            Gender gender,
            string? parmenantAddress,
            string? currentAddress,
            bool isCurrentSameAsParmenantAddress,
            string? personalEmailId,
            string? personalMobileNo,
            string? otherContactNo,
            string employeeCode,
            DateTimeOffset joiningOn,
            Guid createdBy)
        {

            Id = id;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
            ParmenantAddress = parmenantAddress;
            CurrentAddress = currentAddress;
            IsCurrentSameAsParmenantAddress = isCurrentSameAsParmenantAddress;
            PersonalEmailId = personalEmailId;
            PersonalMobileNo = personalMobileNo;
            OtherContactNo = otherContactNo;
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
            DateTimeOffset? birthDate,
            Gender gender,
            string? parmenantAddress,
            string? currentAddress,
            bool isCurrentSameAsParmenantAddress,
            string? personalEmailId,
            string? personalMobileNo,
            string? otherContactNo,
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
            Guard.Against.MaximumLength(parmenantAddress ?? string.Empty, "Permenant Address", 200);
            Guard.Against.MaximumLength(currentAddress ?? string.Empty, "Current Address", 200);
            if (!string.IsNullOrWhiteSpace(personalEmailId))
            {
                Guard.Against.InValidEmailId(personalEmailId, "Persoanl Email Id");
                Guard.Against.MaximumLength(personalEmailId, "Persoanl Email Id", 50);
            }
            Guard.Against.Alphabet(personalMobileNo ?? string.Empty, "Persoanl Mobile No");
            Guard.Against.MaximumLength(personalMobileNo ?? string.Empty, "Persoanl Mobile No", 15);
            Guard.Against.Alphabet(otherContactNo ?? string.Empty, "Other Contact No");
            Guard.Against.MaximumLength(otherContactNo ?? string.Empty, "Other Contact No", 15);

            Guard.Against.NullOrWhiteSpace(employeeCode, "Employee Code");
            Guard.Against.Null(joiningOn, "Joining On");
            Guard.Against.Null(createdBy, "Created By");

            var employeeCodeExist = isEmployeeCodeExist(employeeCode).ConfigureAwait(false).GetAwaiter().GetResult();
            if (employeeCodeExist)
            {
                throw new DomainException("Employee Code is already exist.");
            }

            return new Employee(Guid.NewGuid(), firstName, middleName, lastName, birthDate, gender, parmenantAddress,
                currentAddress, isCurrentSameAsParmenantAddress, personalEmailId, personalMobileNo, otherContactNo,
                employeeCode, joiningOn, createdBy);
        }

        public void UpdateEmployeePersonalDetails(string firstName,
            string middleName,
            string lastName,
            DateTimeOffset? birthDate,
            string? bloodGroup,
            Gender gender,
            string? parmenantAddress,
            string? currentAddress,
            bool isCurrentSameAsParmenantAddress,
            MaritalStatus? maritalStatus,
            string? personalEmailId,
            string? personalMobileNo,
            string? otherContactNo,
            Guid modifiedBy)
        {
            Guard.Against.NullOrWhiteSpace(firstName, "First Name");
            Guard.Against.MaximumLength(firstName, "First Name", 20);
            Guard.Against.NullOrWhiteSpace(middleName, "Middle Name");
            Guard.Against.MaximumLength(middleName, "Middle Name", 20);
            Guard.Against.NullOrWhiteSpace(lastName, "Last Name");
            Guard.Against.MaximumLength(lastName, "Last Name", 20);
            Guard.Against.MaximumLength(bloodGroup ?? string.Empty, "Blood Group", 20);
            Guard.Against.MaximumLength(parmenantAddress ?? string.Empty, "Permenant Address", 200);
            Guard.Against.MaximumLength(currentAddress ?? string.Empty, "Current Address", 200);
            if (!string.IsNullOrWhiteSpace(personalEmailId))
            {
                Guard.Against.InValidEmailId(personalEmailId, "Persoanl Email Id");
                Guard.Against.MaximumLength(personalEmailId, "Persoanl Email Id", 50);
            }
            Guard.Against.Alphabet(personalMobileNo ?? string.Empty, "Persoanl Mobile No");
            Guard.Against.MaximumLength(personalMobileNo ?? string.Empty, "Persoanl Mobile No", 15);
            Guard.Against.Alphabet(otherContactNo ?? string.Empty, "Other Contact No");
            Guard.Against.MaximumLength(otherContactNo ?? string.Empty, "Other Contact No", 15);

            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            BirthDate = birthDate;
            BloodGroup = bloodGroup;
            Gender = gender;
            ParmenantAddress = parmenantAddress;
            CurrentAddress = currentAddress;
            IsCurrentSameAsParmenantAddress = isCurrentSameAsParmenantAddress;
            MaritalStatus = maritalStatus;
            PersonalEmailId = personalEmailId;
            PersonalMobileNo = personalMobileNo;
            OtherContactNo = otherContactNo;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void UpdateEmployeeOfficeDetails(
            string? officeEmailId,
            string? officeContactNo,
            DateTimeOffset joiningOn,
            DateTimeOffset? relievingOn,
            Guid? designationId,
            Guid? reportingToId,
            Guid? departmentId,
            Guid modifiedBy,
            Func<Guid, Task<bool>> isDesignationExist,
            Func<Guid, Task<bool>> isReportingToExist,
            Func<Guid, Task<bool>> isDepartmentExist)
        {
            if (!string.IsNullOrWhiteSpace(officeEmailId))
            {
                Guard.Against.InValidEmailId(officeEmailId, "Official Email Id");
                Guard.Against.MaximumLength(officeEmailId, "Official Email Id", 50);
            }
            Guard.Against.Alphabet(officeContactNo ?? string.Empty, "Official Mobile No");
            Guard.Against.MaximumLength(officeContactNo ?? string.Empty, "Official Mobile No", 15);
            Guard.Against.Null(joiningOn, "Joining On");
            if (relievingOn.HasValue)
            {
                Guard.Against.DateTimeOffsetLessThanOrEqual(relievingOn.Value, "Relieving On", joiningOn);
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

            OfficeEmailId = officeEmailId;
            OfficeContactNo = officeContactNo;
            JoiningOn = joiningOn;
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

        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string? BloodGroup { get; set; }
        public Gender Gender { get; set; }
        public string? ParmenantAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public bool IsCurrentSameAsParmenantAddress { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? PersonalMobileNo { get; set; }
        public string? OtherContactNo { get; set; }
        public string? ProfilePhotoName { get; set; }

        // Office Information
        public string EmployeeCode { get; set; }
        public string? OfficeEmailId { get; set; }
        public string? OfficeContactNo { get; set; }
        public DateTimeOffset JoiningOn { get; set; }
        public DateTimeOffset? RelievingOn { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? ReportingToId { get; set; }
        public Guid? DepartmentId { get; set; }

        public EmployeeBankDetail EmployeeBankDetail { get; set; }
        public ICollection<EmployeeDocument> EmployeeDocuments { get; set; }
        public User User { get; set; }
        public Designation Designation { get; set; }
        public Employee ReportingTo { get; set; }
        public ICollection<Employee> ReportingTos { get; set; }
        public Department Department { get; set; }
        #endregion

        public string GetFullName()
        {
            return string.Format("{0} {1} {2}", FirstName, MiddleName, LastName);
        }

        public string GetNameWithDesignation()
        {
            return string.Format("{0} {1} ({2})", FirstName, LastName, Designation?.Name);
        }
    }
}