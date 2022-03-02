using ERP.Domain.Enums;
using MediatR;

namespace ERP.Application.Modules.Employees
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? ParmenantAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public bool IsCurrentSameAsParmenantAddress { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? PersoanlMobileNo { get; set; }
        public string? OtherContactNo { get; set; }

        // Office Information
        public string EmployeeCode { get; set; }
        public DateTime JoiningOn { get; set; }
    }

    public class UpdateEmployeeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BloodGroup { get; set; }
        public Gender Gender { get; set; }
        public string? ParmenantAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public bool IsCurrentSameAsParmenantAddress { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? PersonalMobileNo { get; set; }
        public string? OtherContactNo { get; set; }

        // Office Information
        public string? OfficeEmailId { get; set; }
        public string? OfficeContactNo { get; set; }
        public DateTime JoiningOn { get; set; }
        public DateTime? RelievingOn { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? ReportingToId { get; set; }
    }
}