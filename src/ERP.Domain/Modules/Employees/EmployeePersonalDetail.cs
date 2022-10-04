using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Enums;

namespace ERP.Domain.Modules.Employees
{
    public class EmployeePersonalDetail : BaseAuditableEntity
    {
        public EmployeePersonalDetail()
        { }

        private EmployeePersonalDetail(Guid id,
            Guid employeeId,
            DateTimeOffset? birthDate,
            Gender gender,
            string? parmenantAddress,
            string? currentAddress,
            bool isCurrentSameAsParmenantAddress,
            string? personalEmailId,
            string? personalMobileNo,
            string? otherContactNo,
            Guid createdBy)
        {

            Id = id;
            EmployeeId = employeeId;
            BirthDate = birthDate;
            Gender = gender;
            ParmenantAddress = parmenantAddress;
            CurrentAddress = currentAddress;
            IsCurrentSameAsParmenantAddress = isCurrentSameAsParmenantAddress;
            PersonalEmailId = personalEmailId;
            PersonalMobileNo = personalMobileNo;
            OtherContactNo = otherContactNo;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public static EmployeePersonalDetail Create(
            Guid employeeId,
            DateTimeOffset? birthDate,
            Gender gender,
            string? parmenantAddress,
            string? currentAddress,
            bool isCurrentSameAsParmenantAddress,
            string? personalEmailId,
            string? personalMobileNo,
            string? otherContactNo,
            Guid createdBy)
        {
            Guard.Against.Null(employeeId, "EmployeeId");
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

            Guard.Against.Null(createdBy, "Created By");

            return new EmployeePersonalDetail(Guid.NewGuid(), employeeId, birthDate, gender, parmenantAddress,
                currentAddress, isCurrentSameAsParmenantAddress, personalEmailId, personalMobileNo,
                otherContactNo, createdBy);
        }

        public void UpdateEmployeePersonalDetail(
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

        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
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

        public Employee Employee { get; protected set; }
    }
}