using ERP.Domain.Core.Repositories;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeeBankDetailQueryHandler : IRequestHandler<GetEmployeeBankDetailReq, EmployeeBankDetailViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeBankDetailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeBankDetailViewModel> Handle(GetEmployeeBankDetailReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeBankDetailSpecifications.GetBankDetailByEmployeeIdSpec(request.EmployeeId);
            var bankDetails = await _unitOfWork.Repository<EmployeeBankDetail>().FirstOrDefaultAsync(spec, false);

            if (bankDetails == null)
            {
                return null;
            }

            return new EmployeeBankDetailViewModel
            {
                Id = bankDetails.Id,
                EmployeeId = bankDetails.EmployeeId,
                BankName = bankDetails.BankName,
                IFSCCode = bankDetails.IFSCCode,
                BranchAddress = bankDetails.BranchAddress,
                AccountNumber = bankDetails.AccountNumber,
                PANNumber = bankDetails.PANNumber,
                PFNumber = bankDetails.PFNumber,
                UANNumber = bankDetails.UANNumber,
            };
        }
    }
}