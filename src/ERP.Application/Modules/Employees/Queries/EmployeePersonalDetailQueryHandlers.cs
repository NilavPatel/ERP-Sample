using ERP.Domain.Core.Repositories;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeePersonalDetailByIdQueryHandler : IRequestHandler<GetEmployeePersonalDetailByIdReq, EmployeePersonalDetailViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeePersonalDetailByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeePersonalDetailViewModel> Handle(GetEmployeePersonalDetailByIdReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeePersonalDetailSpecifications.GetPersonalDetailByIdSpec(request.EmployeeId);
            var employee = await _unitOfWork.Repository<EmployeePersonalDetail>().SingleAsync(spec, false);

            return new EmployeePersonalDetailViewModel
            {
                Id = employee.Id,
                BirthDate = employee.BirthDate,
                BloodGroup = employee.BloodGroup,
                Gender = employee.Gender,
                GenderText = employee.Gender.ToString(),
                ParmenantAddress = employee.ParmenantAddress,
                CurrentAddress = employee.CurrentAddress,
                IsCurrentSameAsParmenantAddress = employee.IsCurrentSameAsParmenantAddress,
                MaritalStatus = employee.MaritalStatus,
                MaritalStatusText = employee.MaritalStatus.ToString(),
                PersonalEmailId = employee.PersonalEmailId,
                PersonalMobileNo = employee.PersonalMobileNo,
                OtherContactNo = employee.OtherContactNo
            };
        }
    }

}