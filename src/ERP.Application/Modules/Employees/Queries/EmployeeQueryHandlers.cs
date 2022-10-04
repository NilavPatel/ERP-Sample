using ERP.Domain.Core.Repositories;
using ERP.Domain.Modules.Employees;
using ERP.Domain.Core.Specifications;
using MediatR;
using ERP.Application.Core.Models;

namespace ERP.Application.Modules.Employees.Queries
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
            spec.AddInclude(x => x.ReportingTo.Designation);
            spec.AddInclude(x => x.Designation);
            if (request.PageSize > 0)
            {
                spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            }
            var data = await _unitOfWork.Repository<Employee>().ListAsync(spec, false);

            return new GetAllEmployeesRes
            {
                Result = data.Select(employee => new EmployeeViewModel
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    MiddleName = employee.MiddleName,
                    LastName = employee.LastName,
                    FullName = employee.GetNameWithDesignation(),
                    EmployeeCode = employee.EmployeeCode,
                    OfficeEmailId = employee.OfficeEmailId,
                    OfficeContactNo = employee.OfficeContactNo,
                    JoiningOn = employee.JoiningOn,
                    ConfirmationOn = employee.ConfirmationOn,
                    ResignationOn = employee.ResignationOn,
                    RelievingOn = employee.RelievingOn,
                    DesignationId = employee.DesignationId,
                    DesignationName = employee.Designation?.Name,
                    ReportingToName = employee.ReportingTo?.GetNameWithDesignation(),
                    ReportingToId = employee.ReportingToId,
                    IsUserCreated = employee.User != null,
                    ProfilePhotoName = employee.ProfilePhotoName
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
            spec.AddInclude(x => x.Department);
            var employee = await _unitOfWork.Repository<Employee>().SingleAsync(spec, false);

            return new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                MiddleName = employee.MiddleName,
                LastName = employee.LastName,
                FullName = employee.GetNameWithDesignation(),
                EmployeeCode = employee.EmployeeCode,
                OfficeEmailId = employee.OfficeEmailId,
                OfficeContactNo = employee.OfficeContactNo,
                JoiningOn = employee.JoiningOn,
                ConfirmationOn = employee.ConfirmationOn,
                ResignationOn = employee.ResignationOn,
                RelievingOn = employee.RelievingOn,
                DesignationId = employee.DesignationId,
                DesignationName = employee.Designation?.Name,
                ReportingToId = employee.ReportingToId,
                ReportingToName = employee.ReportingTo?.GetNameWithDesignation(),
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.Name,
                IsUserCreated = employee.User != null,
                ProfilePhotoName = employee.ProfilePhotoName
            };
        }
    }

    public class GetAvailableReportingPersonsQueryHandler : IRequestHandler<GetAvailableReportingPersonsReq, IList<SelectListItem>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAvailableReportingPersonsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<SelectListItem>> Handle(GetAvailableReportingPersonsReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetAllEmployeesExceptIdSpec(request.EmployeeId, request.SearchKeyword);
            spec.AddInclude(x => x.Designation);
            if (request.PageSize > 0)
            {
                spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            }
            var data = await _unitOfWork.Repository<Employee>().ListAsync(spec, false);
            return data.Select(employee => new SelectListItem
            {
                value = employee.Id.ToString(),
                text = employee.GetNameWithDesignation()
            }).ToList();
        }
    }
}