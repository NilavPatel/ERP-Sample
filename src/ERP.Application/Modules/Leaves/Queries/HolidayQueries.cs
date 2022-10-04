using ERP.Application.Core.Models;
using ERP.Domain.Modules.Leaves;
using MediatR;

namespace ERP.Application.Modules.Leaves.Queries
{
    public class GetAllHolidaysReq : PagedListReq, IRequest<GetAllHolidaysRes>
    { }

    public class GetAllHolidaysRes : PagedListRes<Holiday>
    { }

    public class GetHolidayByIdReq : IRequest<Holiday>
    {
        public Guid Id { get; set; }
    }

    public class GetAllHolidayByYearReq : IRequest<IList<Holiday>>
    {
        public int Year { get; set; }
    }
}