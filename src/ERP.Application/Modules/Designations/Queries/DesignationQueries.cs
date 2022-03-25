using ERP.Application.Core.Models;
using ERP.Domain.Modules.Designations;
using MediatR;

namespace ERP.Application.Modules.Designations.Queries
{
    public class GetAllDesignationsReq : PagedListReq, IRequest<GetAllDesignationsRes>
    { }

    public class GetAllDesignationsRes : PagedListRes<Designation>
    { }

    public class GetDesignationByIdReq : IRequest<Designation>
    {
        public Guid Id { get; set; }
    }
}