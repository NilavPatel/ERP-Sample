using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles.Commands
{
    public class AddRolePermissionsCommnd : IRequest<Guid>
    {
        public Guid RoleId { get; set; }
        public IList<int> Permissions { get; set; }
    }
}