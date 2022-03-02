using ERP.Application.Core.Repositories;
using ERP.Domain.Modules.Employees;
using ERP.Domain.Core.Specifications;
using MediatR;
using ERP.Domain.Exceptions;

namespace ERP.Application.Modules.Employees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesReq, GetAllEmployeesRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllEmployeesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllEmployeesRes> Handle(GetAllEmployeesReq request, CancellationToken cancellationToken)
        {
            BaseSpecification<Employee> spec;
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                spec = EmployeeSpecifications.SearchEmployeesSpec(request.SearchKeyword);
            }
            else
            {
                spec = EmployeeSpecifications.GetAllEmployeesSpec();
            }
            var count = await _unitOfWork.Repository<Employee>().CountAsync(spec);
            spec.AddInclude(x => x.User);
            spec.AddInclude(x => x.ReportingTo);
            spec.AddInclude(x => x.Designation);
            spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            var data = await _unitOfWork.Repository<Employee>().ListAsync(spec, false);

            return new GetAllEmployeesRes
            {
                Result = data.Select(employee => new EmployeeViewModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
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
                    OtherContactNo = employee.OtherContactNo,
                    EmployeeCode = employee.EmployeeCode,
                    OfficeEmailId = employee.OfficeEmailId,
                    OfficeContactNo = employee.OfficeContactNo,
                    JoiningOn = employee.JoiningOn,
                    RelievingOn = employee.RelievingOn,
                    DesignationId = employee.DesignationId,
                    DesignationName = employee.Designation?.Name,
                    ReportingToName = employee.ReportingTo?.GetNameWithDesignation(),
                    ReportingToId = employee.ReportingToId,
                    IsUserCreated = employee.User != null
                }).ToList(),
                Count = count
            };
        }
    }

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdReq, EmployeeViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeViewModel> Handle(GetEmployeeByIdReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(request.Id);
            spec.AddInclude(x => x.User);
            spec.AddInclude(x => x.ReportingTo);
            spec.AddInclude(x => x.ReportingTo.Designation);
            spec.AddInclude(x => x.Designation);
            var employee = await _unitOfWork.Repository<Employee>().FirstOrDefaultAsync(spec, false);
            if (employee == null)
            {
                throw new RecordNotFoundException("Employee Not Found.");
            }
            return new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
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
                OtherContactNo = employee.OtherContactNo,
                EmployeeCode = employee.EmployeeCode,
                OfficeEmailId = employee.OfficeEmailId,
                OfficeContactNo = employee.OfficeContactNo,
                JoiningOn = employee.JoiningOn,
                RelievingOn = employee.RelievingOn,
                DesignationId = employee.DesignationId,
                DesignationName = employee.Designation?.Name,
                ReportingToId = employee.ReportingToId,
                ReportingToName = employee.ReportingTo?.GetNameWithDesignation(),
                IsUserCreated = employee.User != null
            };
        }
    }

    public class GetAvailableReportingPersonsQueryHandler : IRequestHandler<GetAvailableReportingPersonsReq, IList<EmployeeViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAvailableReportingPersonsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<EmployeeViewModel>> Handle(GetAvailableReportingPersonsReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetAllEmployeesExceptIdSpec(request.EmployeeId, request.SearchKeyword);
            spec.AddInclude(x => x.Designation);
            spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            var data = await _unitOfWork.Repository<Employee>().ListAsync(spec, false);
            return data.Select(employee => new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                DesignationName = employee.Designation?.Name,
            }).ToList();
        }
    }
}