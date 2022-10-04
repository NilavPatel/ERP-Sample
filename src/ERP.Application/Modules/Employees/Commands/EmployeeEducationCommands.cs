using MediatR;

namespace ERP.Application.Modules.Employees.Commands
{
    public class AddEmployeeEducationCommand : IRequest<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string Degree { get; set; }
        public string InstituteName { get; set; }
        public int PassingMonth { get; set; }
        public int PassingYear { get; set; }
        public int Percentage { get; set; }
    }

    public class UpdateEmployeeEducationCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Degree { get; set; }
        public string InstituteName { get; set; }
        public int PassingMonth { get; set; }
        public int PassingYear { get; set; }
        public int Percentage { get; set; }
    }

    public class RemoveEmployeeEducationCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}