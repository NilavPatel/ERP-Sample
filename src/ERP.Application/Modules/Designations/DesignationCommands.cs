using MediatR;

namespace ERP.Application.Modules.Designations
{
    public class CreateDesignation : IRequest<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateDesignation : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}