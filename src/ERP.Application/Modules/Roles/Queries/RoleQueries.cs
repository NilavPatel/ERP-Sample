using ERP.Application.Core.Models;
using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles.Queries
{
    public class GetAllRolesReq : PagedListReq, IRequest<GetAllRolesRes>
    { }

    public class GetAllRolesRes : PagedListRes<RoleViewModel>
    { }

    public class GetRoleByIdReq : IRequest<RoleViewModel>
    {
        public Guid Id { get; set; }
    }

    public class RoleViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}