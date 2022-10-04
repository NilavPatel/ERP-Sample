using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Commands
{
    public class UpdateEmployeePersonalDetailCommandHandler : BaseCommandHandler, IRequestHandler<UpdateEmployeePersonalDetailCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeePersonalDetailCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext userContext) : base(mediator, userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateEmployeePersonalDetailCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeePersonalDetailSpecifications.GetPersonalDetailByIdSpec(request.Id);
            var employeePersonalDetail = await _unitOfWork.Repository<EmployeePersonalDetail>().SingleAsync(spec, true);

            employeePersonalDetail.UpdateEmployeePersonalDetail(
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
                 GetCurrentEmployeeId()
                 );

            _unitOfWork.Repository<EmployeePersonalDetail>().Update(employeePersonalDetail);
            await _unitOfWork.SaveChangesAsync();

            return employeePersonalDetail.Id;
        }
    }

}