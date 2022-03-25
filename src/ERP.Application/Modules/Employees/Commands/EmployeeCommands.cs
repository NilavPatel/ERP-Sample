using ERP.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ERP.Application.Modules.Employees.Commands
{
    public class CreateEmployeeCommand : IRequest<Guid>
    {
        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public Gender Gender { get; set; }
        public string? ParmenantAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public bool IsCurrentSameAsParmenantAddress { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? PersonalMobileNo { get; set; }
        public string? OtherContactNo { get; set; }

        // Office Information
        public string EmployeeCode { get; set; }
        public DateTimeOffset JoiningOn { get; set; }
    }

    public class UpdateEmployeePersonalDetailsCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
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
    }

    public class UpdateEmployeeOfficeDetailsCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? OfficeEmailId { get; set; }
        public string? OfficeContactNo { get; set; }
        public DateTimeOffset JoiningOn { get; set; }
        public DateTimeOffset? RelievingOn { get; set; }
        public Guid? DesignationId { get; set; }
        public Guid? ReportingToId { get; set; }
        public Guid? DepartmentId { get; set; }
    }

    public class UploadEmployeeProfilePhotoCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public IFormFile Photo { get; set; }
    }

    public class RemoveEmployeeProfilePhotoCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}