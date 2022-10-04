using ERP.Application.Core.Models;
using ERP.Domain.Modules.Leaves;
using MediatR;

namespace ERP.Application.Modules.Leaves.Queries
{
    public class GetAllLeaveTypesReq : PagedListReq, IRequest<GetAllLeaveTypesRes>
    { }

    public class GetAllLeaveTypesRes : PagedListRes<LeaveType>
    { }

    public class GetLeaveTypeByIdReq : IRequest<LeaveType>
    {
        public Guid Id { get; set; }
    }

    public class GetAllActiveLeaveTypesReq : IRequest<IList<LeaveType>>
    { }
}