using ERP.Application.Core;
using ERP.Application.Core.Repositories;
using ERP.Application.Core.Services;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees
{
    public class AddEmployeeBankDetailCommandHandler : BaseCommandHandler, IRequestHandler<AddEmployeeBankDetailCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddEmployeeBankDetailCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddEmployeeBankDetailCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(request.EmployeeId);
            var employee = await _unitOfWork.Repository<Employee>().FirstOrDefaultAsync(spec, false);
            if (employee == null)
            {
                throw new RecordNotFoundException("Employee Not Found");
            }

            var bankDetailsSpec = EmployeeBankDetailSpecifications.GetBankDetailByEmployeeId(request.EmployeeId);
            var existingBankDetails = await _unitOfWork.Repository<EmployeeBankDetail>().FirstOrDefaultAsync(bankDetailsSpec, false);
            if (existingBankDetails != null)
            {
                throw new RecordNotFoundException("Employee Bank Details Already Exist");
            }

            var bankDetails = EmployeeBankDetail.CreateBankDetails(
                request.EmployeeId,
                request.BankName,
                request.IFSCCode,
                request.BranchAddress,
                request.AccountNumber,
                request.PANNumber,
                GetUserId());

            await _unitOfWork.Repository<EmployeeBankDetail>().AddAsync(bankDetails);
            await _unitOfWork.SaveChangesAsync();

            return employee.Id;
        }
    }

    public class UpdateEmployeeBankDetailCommandHandler : BaseCommandHandler, IRequestHandler<UpdateEmployeeBankDetailCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeBankDetailCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateEmployeeBankDetailCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeBankDetailSpecifications.GetBankDetailByEmployeeId(request.EmployeeId);
            var bankDetails = await _unitOfWork.Repository<EmployeeBankDetail>().FirstOrDefaultAsync(spec, true);
            if (bankDetails == null)
            {
                throw new RecordNotFoundException("Employee Bank Details Not Found");
            }

            bankDetails.UpdateBankDetails(
                request.BankName,
                request.IFSCCode,
                request.BranchAddress,
                request.AccountNumber,
                request.PANNumber,
                GetUserId());

            _unitOfWork.Repository<EmployeeBankDetail>().Update(bankDetails);
            await _unitOfWork.SaveChangesAsync();

            return bankDetails.Id;
        }
    }
}