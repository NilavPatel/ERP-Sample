using MediatR;

namespace ERP.Application.Modules.Employees.Commands
{
    public class AddEmployeeBankDetailCommand : IRequest<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string? BankName { get; set; }
        public string? IFSCCode { get; set; }
        public string? BranchAddress { get; set; }
        public string? AccountNumber { get; set; }
        public string? PANNumber { get; set; }
    }

    public class UpdateEmployeeBankDetailCommand : IRequest<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string? BankName { get; set; }
        public string? IFSCCode { get; set; }
        public string? BranchAddress { get; set; }
        public string? AccountNumber { get; set; }
        public string? PANNumber { get; set; }
    }
}