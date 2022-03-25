using ERP.Domain.Core.Repositories;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
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
            var spec = EmployeeBankDetailSpecifications.GetBankDetailByEmployeeIdSpec(request.EmployeeId);
            return await _unitOfWork.Repository<EmployeeBankDetail>().FirstOrDefaultAsync(spec, false);
        }
    }
}