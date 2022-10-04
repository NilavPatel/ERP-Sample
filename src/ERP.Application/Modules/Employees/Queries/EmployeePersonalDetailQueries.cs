using ERP.Domain.Enums;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeePersonalDetailByIdReq : IRequest<EmployeePersonalDetailViewModel>
    {
        public Guid EmployeeId { get; set; }
    }

    public class EmployeePersonalDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public string? BloodGroup { get; set; }
        public Gender Gender { get; set; }
        public string GenderText { get; set; }
        public string? ParmenantAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public bool IsCurrentSameAsParmenantAddress { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? MaritalStatusText { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? PersonalMobileNo { get; set; }
        public string? OtherContactNo { get; set; }
    }
}