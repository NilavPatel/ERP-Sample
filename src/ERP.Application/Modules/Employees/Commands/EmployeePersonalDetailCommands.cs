using ERP.Domain.Enums;
using MediatR;

namespace ERP.Application.Modules.Employees.Commands
{
    public class UpdateEmployeePersonalDetailCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }       
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
}