using ERP.Application.Core.Models;
using ERP.Domain.Modules.Departments;
using MediatR;

namespace ERP.Application.Modules.Departments.Queries
{
    public class GetAllDepartmentsReq : PagedListReq, IRequest<GetAllDepartmentsRes>
    { }

    public class GetAllDepartmentsRes : PagedListRes<Department>
    { }

    public class GetDepartmentByIdReq : IRequest<Department>
    {
        public Guid Id { get; set; }
    }
}