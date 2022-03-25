using MediatR;

namespace ERP.Application.Modules.Roles.Commands
{
    public class CreateRoleCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateRoleCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class DeleteRoleCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}