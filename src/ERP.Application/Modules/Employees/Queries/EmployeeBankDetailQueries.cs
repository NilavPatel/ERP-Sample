using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeeBankDetailReq : IRequest<EmployeeBankDetailViewModel>
    {
        public Guid EmployeeId { get; set; }
    }

    public class EmployeeBankDetailViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string? BankName { get; set; }
        public string? IFSCCode { get; set; }
        public string? BranchAddress { get; set; }
        public string? AccountNumber { get; set; }
        public string? PANNumber { get; set; }
        public string? PFNumber { get; set; }
        public string? UANNumber { get; set; }
    }
}