using ERP.Application.Core.Models;
using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles.Queries
{
    public class GetAllRolesReq : PagedListReq, IRequest<GetAllRolesRes>
    { }

    public class GetAllRolesRes : PagedListRes<Role>
    { }

    public class GetRoleByIdReq : IRequest<Role>
    {
        public Guid Id { get; set; }
    }
}