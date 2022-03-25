using MediatR;

namespace ERP.Application.Modules.Designations.Commands
{
    public class CreateDesignationCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateDesignationCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class DeleteDesignationCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}