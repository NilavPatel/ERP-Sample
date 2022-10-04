using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;

namespace ERP.Domain.Modules.Employees
{
    public class EmployeeBankDetail : BaseAuditableEntity
    {
        public EmployeeBankDetail()
        { }

        private EmployeeBankDetail(Guid id, Guid employeeId, string? bankName, string? ifscCode, string? branchAddress,
            string? accountNumber, string? panNumber, string? pfNumber, string? uanNumber, Guid createdBy)
        {
            Id = id;
            EmployeeId = employeeId;
            BankName = bankName;
            IFSCCode = ifscCode;
            BranchAddress = branchAddress;
            AccountNumber = accountNumber;
            PANNumber = panNumber;
            PFNumber = pfNumber;
            UANNumber = uanNumber;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public static EmployeeBankDetail CreateBankDetails(Guid employeeId, string? bankName, string? ifscCode, string? branchAddress,
            string? accountNumber, string? panNumber, string? pfNumber, string? uanNumber, Guid createdBy)
        {
            Guard.Against.Null(employeeId, "Employee");
            Guard.Against.MaximumLength(bankName ?? string.Empty, "Bank Name", 50);
            Guard.Against.MaximumLength(ifscCode ?? string.Empty, "IFSC Code", 20);
            Guard.Against.MaximumLength(branchAddress ?? string.Empty, "Branch Address", 200);
            Guard.Against.MaximumLength(accountNumber ?? string.Empty, "Account Number", 50);
            Guard.Against.MaximumLength(panNumber ?? string.Empty, "PAN Number", 50);
            Guard.Against.MaximumLength(pfNumber ?? string.Empty, "PF Number", 50);
            Guard.Against.MaximumLength(uanNumber ?? string.Empty, "UAN Number", 50);
            Guard.Against.Null(createdBy, "Created By");

            return new EmployeeBankDetail(Guid.NewGuid(), employeeId, bankName, ifscCode, branchAddress,
                accountNumber, panNumber, pfNumber, uanNumber, createdBy);
        }

        public void UpdateBankDetails(string? bankName, string? ifscCode, string? branchAddress,
            string? accountNumber, string? panNumber, string? pfNumber, string? uanNumber, Guid modifiedBy)
        {
            Guard.Against.MaximumLength(bankName ?? string.Empty, "Bank Name", 50);
            Guard.Against.MaximumLength(ifscCode ?? string.Empty, "IFSC Code", 20);
            Guard.Against.MaximumLength(branchAddress ?? string.Empty, "Branch Address", 200);
            Guard.Against.MaximumLength(accountNumber ?? string.Empty, "Account Number", 50);
            Guard.Against.MaximumLength(panNumber ?? string.Empty, "PAN Number", 50);
            Guard.Against.MaximumLength(pfNumber ?? string.Empty, "PF Number", 50);
            Guard.Against.MaximumLength(uanNumber ?? string.Empty, "UAN Number", 50);
            Guard.Against.Null(modifiedBy, "Modified By");

            BankName = bankName;
            IFSCCode = ifscCode;
            BranchAddress = branchAddress;
            AccountNumber = accountNumber;
            PANNumber = panNumber;
            PFNumber = pfNumber;
            UANNumber = uanNumber;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        #region State
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string? BankName { get; set; }
        public string? IFSCCode { get; set; }
        public string? BranchAddress { get; set; }
        public string? AccountNumber { get; set; }
        public string? PANNumber { get; set; }
        public string? PFNumber { get; set; }
        public string? UANNumber { get; set; }

        public Employee Employee { get; protected set; }

        #endregion
    }
}