using ERP.Application.Core.Repositories;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees
{
    public class GetEmployeeBankDetailQueryHandler : IRequestHandler<GetEmployeeBankDetailReq, EmployeeBankDetail>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeBankDetailQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeBankDetail> Handle(GetEmployeeBankDetailReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeBankDetailSpecifications.GetBankDetailByEmployeeId(request.EmployeeId);
            return await _unitOfWork.Repository<EmployeeBankDetail>().FirstOrDefaultAsync(spec, false);
        }
    }
}