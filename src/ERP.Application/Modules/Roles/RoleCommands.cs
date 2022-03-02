using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles
{
    public class CreateRole : IRequest<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateRole : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}