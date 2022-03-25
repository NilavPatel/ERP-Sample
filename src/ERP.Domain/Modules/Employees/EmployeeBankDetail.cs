using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;

namespace ERP.Domain.Modules.Employees
{
    public class EmployeeBankDetail : BaseAuditableEntity
    {
        public EmployeeBankDetail()
        { }

        private EmployeeBankDetail(Guid employeeId, string? bankName, string? ifscCode, string? branchAddress,
            string? accountNumber, string? panNumber, Guid createdBy)
        {
            Id = Guid.NewGuid();
            EmployeeId = employeeId;
            BankName = bankName;
            IFSCCode = ifscCode;
            BranchAddress = branchAddress;
            AccountNumber = accountNumber;
            PANNumber = panNumber;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public static EmployeeBankDetail CreateBankDetails(Guid employeeId, string? bankName, string? ifscCode, string? branchAddress,
            string? accountNumber, string? panNumber, Guid createdBy)
        {
            Guard.Against.Null(employeeId, "Employee");
            Guard.Against.MaximumLength(bankName ?? string.Empty, "Bank Name", 50);
            Guard.Against.MaximumLength(ifscCode ?? string.Empty, "IFSC Code", 20);
            Guard.Against.MaximumLength(branchAddress ?? string.Empty, "Branch Address", 200);
            Guard.Against.MaximumLength(accountNumber ?? string.Empty, "Account Number", 50);
            Guard.Against.MaximumLength(panNumber ?? string.Empty, "PAN Number", 50);
            Guard.Against.Null(createdBy, "Created By");

            return new EmployeeBankDetail(employeeId, bankName, ifscCode, branchAddress,
                accountNumber, panNumber, createdBy);
        }

        public void UpdateBankDetails(string? bankName, string? ifscCode, string? branchAddress,
            string? accountNumber, string? panNumber, Guid modifiedBy)
        {
            Guard.Against.MaximumLength(bankName ?? string.Empty, "Bank Name", 50);
            Guard.Against.MaximumLength(ifscCode ?? string.Empty, "IFSC Code", 20);
            Guard.Against.MaximumLength(branchAddress ?? string.Empty, "Branch Address", 200);
            Guard.Against.MaximumLength(accountNumber ?? string.Empty, "Account Number", 50);
            Guard.Against.MaximumLength(panNumber ?? string.Empty, "PAN Number", 50);
            Guard.Against.Null(modifiedBy, "Modified By");

            BankName = bankName;
            IFSCCode = ifscCode;
            BranchAddress = branchAddress;
            AccountNumber = accountNumber;
            PANNumber = panNumber;
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

        public Employee Employee { get; protected set; }

        #endregion
    }
}