using ERP.Domain.Enums;
using MediatR;

namespace ERP.Application.Modules.Employees
{
    public class GetAllEmployeesReq : IRequest<GetAllEmployeesRes>
    {
        public string SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllEmployeesRes
    {
        public IList<EmployeeViewModel> Result { get; set; }
        public int Count { get; set; }
    }

    public class GetEmployeeByIdReq : IRequest<EmployeeViewModel>
    {
        public Guid Id { get; set; }
    }

    public class GetAvailableReportingPersonsReq : IRequest<IList<EmployeeViewModel>>
    {
        public Guid EmployeeId { get; set; }
        public string SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class EmployeeViewModel
    {
        public Guid Id { get; set; }

        // Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? BloodGroup { get; set; }
        public Gender Gender { get; set; }
        public string GenderText { get; set; }
        public string? ParmenantAddress { get; set; }
        public string? CurrentAddress { get; set; }
        public bool IsCurrentSameAsParmenantAddress { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public string? MaritalStatusText { get; set; }
        public string? PersonalEmailId { get; set; }
        public string? PersonalMobileNo { get; set; }
        public string? OtherContactNo { get; set; }

        // Office Information
        public string EmployeeCode { get; set; }
        public string? OfficeEmailId { get; set; }
        public string? OfficeContactNo { get; set; }
        public DateTime JoiningOn { get; set; }
        public DateTime? RelievingOn { get; set; }
        public Guid? DesignationId { get; set; }
        public string? DesignationName { get; set; }
        public Guid? ReportingToId { get; set; }
        public string? ReportingToName { get; set; }

        public bool IsUserCreated { get; set; }
    }
}