using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles
{
    public class GetAllRolePermissionByRoleId : IRequest<IList<RolePermissionViewModel>>
    {
        public Guid RoleId { get; set; }
    }

    public class RolePermissionViewModel
    {
        public int Id { get; set; }
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public bool HasPermission { get; set; }
    }
}