using ERP.Domain.Core.Repositories;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeeEducationsQueryHandler : IRequestHandler<GetEmployeeEducationsReq, IList<EmployeeEducationViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeEducationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<EmployeeEducationViewModel>> Handle(GetEmployeeEducationsReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeEducationSpecifications.GetEducationByEmployeeIdSpec(request.EmployeeId);
            var data = await _unitOfWork.Repository<EmployeeEducationDetail>().ListAsync(spec, false);
            return data.Select(x => new EmployeeEducationViewModel
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                Degree = x.Degree,
                InstituteName = x.InstituteName,
                PassingMonth = x.PassingMonth,
                PassingYear = x.PassingYear,
                Percentage = x.Percentage
            }).ToList();
        }
    }

    public class GetEmployeeEducationByIdQueryHandler : IRequestHandler<GetEmployeeEducationByIdReq, EmployeeEducationViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeEducationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeEducationViewModel> Handle(GetEmployeeEducationByIdReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeEducationSpecifications.GetEmployeeEducationByIdSpec(request.Id);
            var employeeEducation = await _unitOfWork.Repository<EmployeeEducationDetail>().SingleAsync(spec, false);
            return new EmployeeEducationViewModel
            {
                Id = employeeEducation.Id,
                EmployeeId = employeeEducation.EmployeeId,
                Degree = employeeEducation.Degree,
                InstituteName = employeeEducation.InstituteName,
                PassingMonth = employeeEducation.PassingMonth,
                PassingYear = employeeEducation.PassingYear,
                Percentage = employeeEducation.Percentage
            };
        }
    }

}