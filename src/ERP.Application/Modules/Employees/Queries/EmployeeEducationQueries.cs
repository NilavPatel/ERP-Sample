using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeeEducationsReq : IRequest<IList<EmployeeEducationViewModel>>
    {
        public Guid EmployeeId { get; set; }
    }

    public class GetEmployeeEducationByIdReq : IRequest<EmployeeEducationViewModel>
    {
        public Guid Id { get; set; }
    }

    public class EmployeeEducationViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string Degree { get; set; }
        public string InstituteName { get; set; }
        public int PassingMonth { get; set; }
        public int PassingYear { get; set; }
        public int Percentage { get; set; }
    }

}