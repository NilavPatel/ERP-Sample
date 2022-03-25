using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeeBankDetailReq : IRequest<EmployeeBankDetail>
    {
        public Guid EmployeeId { get; set; }
    }
}