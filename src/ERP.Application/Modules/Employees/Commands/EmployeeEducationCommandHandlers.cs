using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Commands
{

    public class RemoveEmployeeEducationCommandHandler : BaseCommandHandler, IRequestHandler<RemoveEmployeeEducationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveEmployeeEducationCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RemoveEmployeeEducationCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeEducationSpecifications.GetEmployeeEducationByIdSpec(request.Id);
            var employeeEducation = await _unitOfWork.Repository<EmployeeEducationDetail>().SingleAsync(spec, true);

            employeeEducation.Remove(GetCurrentEmployeeId());

            _unitOfWork.Repository<EmployeeEducationDetail>().Update(employeeEducation);
            await _unitOfWork.SaveChangesAsync();

            return request.Id;
        }
    }

    public class AddEmployeeEducationCommandHandler : BaseCommandHandler, IRequestHandler<AddEmployeeEducationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddEmployeeEducationCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddEmployeeEducationCommand request, CancellationToken cancellationToken)
        {


            var employeeEducation = EmployeeEducationDetail.Create(Guid.NewGuid(),
                request.EmployeeId,
                request.Degree,
                request.InstituteName,
                request.PassingMonth,
                request.PassingYear,
                request.Percentage,
                GetCurrentEmployeeId(),
                IsEmployeeExist);

            await _unitOfWork.Repository<EmployeeEducationDetail>().AddAsync(employeeEducation);
            await _unitOfWork.SaveChangesAsync();

            return employeeEducation.Id;
        }

        private async Task<bool> IsEmployeeExist(Guid employeeId)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(employeeId);
            var employee = await _unitOfWork.Repository<Employee>().FirstOrDefaultAsync(spec, false);
            return employee != null;
        }
    }

    public class UpdateEmployeeEducationCommandHandler : BaseCommandHandler, IRequestHandler<UpdateEmployeeEducationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeEducationCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateEmployeeEducationCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeEducationSpecifications.GetEmployeeEducationByIdSpec(request.Id);
            var employeeEducation = await _unitOfWork.Repository<EmployeeEducationDetail>().SingleAsync(spec, true);

            employeeEducation.Update(request.Degree,
                request.InstituteName,
                request.PassingMonth,
                request.PassingYear,
                request.Percentage,
                GetCurrentEmployeeId());

            _unitOfWork.Repository<EmployeeEducationDetail>().Update(employeeEducation);
            await _unitOfWork.SaveChangesAsync();

            return employeeEducation.Id;
        }
    }

}