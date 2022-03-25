using MediatR;

namespace ERP.Application.Modules.Departments.Commands
{
    public class CreateDepartmentCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class UpdateDepartmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }

    public class DeleteDepartmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}