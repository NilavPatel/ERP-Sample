using ERP.Application.Core;
using MediatR;
using ERP.Application.Core.Repositories;
using ERP.Domain.Exceptions;
using ERP.Application.Core.Services;
using ERP.Domain.Modules.Employees;
using ERP.Domain.Modules.Designations;

namespace ERP.Application.Modules.Employees
{
    public class CreateEmployeeCommandHandler : BaseCommandHandler, IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(IMediator mediator, IUserContext _userContext, IUnitOfWork unitOfWork) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = Employee.Create(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.BirthDate,
                request.Gender,
                request.ParmenantAddress,
                request.CurrentAddress,
                request.IsCurrentSameAsParmenantAddress,
                request.PersonalEmailId,
                request.PersoanlMobileNo,
                request.OtherContactNo,
                request.EmployeeCode,
                request.JoiningOn,
                GetUserId(),
                IsEmployeeCodeExist);

            await _unitOfWork.Repository<Employee>().AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<bool> IsEmployeeCodeExist(string employeeCode)
        {
            var spec = EmployeeSpecifications.GetEmployeeByEmployeeCodeSpec(employeeCode);
            var employee = await _unitOfWork.Repository<Employee>().FirstOrDefaultAsync(spec, false);
            return employee != null;
        }
    }

    public class UpdateEmployeeCommandHandler : BaseCommandHandler, IRequestHandler<UpdateEmployeeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(request.Id);
            var employee = await _unitOfWork.Repository<Employee>().FirstOrDefaultAsync(spec, true);
            if (employee == null)
            {
                throw new RecordNotFoundException("Employee Not Found");
            }

            employee.UpdateEmployee(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.BirthDate,
                request.BloodGroup,
                request.Gender,
                request.ParmenantAddress,
                request.CurrentAddress,
                request.IsCurrentSameAsParmenantAddress,
                request.MaritalStatus,
                request.PersonalEmailId,
                request.PersonalMobileNo,
                request.OtherContactNo,
                request.OfficeEmailId,
                request.OfficeContactNo,
                request.JoiningOn,
                request.RelievingOn,
                request.DesignationId,
                request.ReportingToId,
                GetUserId(),
                IsDesignationExist,
                IsReportingToExist);

            _unitOfWork.Repository<Employee>().Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<bool> IsDesignationExist(Guid designationId)
        {
            var spec = DesignationSpecifications.GetDesignationByIdSpec(designationId);
            var designation = await _unitOfWork.Repository<Designation>().FirstOrDefaultAsync(spec, false);
            return designation != null;
        }

        public async Task<bool> IsReportingToExist(Guid employeeId)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(employeeId);
            var employee = await _unitOfWork.Repository<Employee>().FirstOrDefaultAsync(spec, false);
            return employee != null;
        }
    }
}