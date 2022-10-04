using ERP.Application.Core.Models;
using ERP.Domain.Enums;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetAllEmployeesReq : PagedListReq, IRequest<GetAllEmployeesRes>
    { }

    public class GetAllEmployeesRes : PagedListRes<EmployeeViewModel>
    { }

    public class GetEmployeeByIdReq : IRequest<EmployeeViewModel>
    {
        public Guid Id { get; set; }
    }

    public class GetAvailableReportingPersonsReq : IRequest<IList<SelectListItem>>
    {
        public Guid EmployeeId { get; set; }
        public string? SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class EmployeeViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName { get; set; }
        public string EmployeeCode { get; set; }
        public string? OfficeEmailId { get; set; }
        public string? OfficeContactNo { get; set; }
        public DateTimeOffset JoiningOn { get; set; }
        public DateTimeOffset? ConfirmationOn { get; set; }
        public DateTimeOffset? ResignationOn { get; set; }
        public DateTimeOffset? RelievingOn { get; set; }
        public Guid? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public Guid? ReportingToId { get; set; }
        public string? ReportingToName { get; set; }
        public Guid? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public bool IsUserCreated { get; set; }
        public string? ProfilePhotoName { get; set; }
    }
}
