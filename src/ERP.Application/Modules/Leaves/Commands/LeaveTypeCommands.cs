using MediatR;

namespace ERP.Application.Modules.Leaves.Commands
{
    public class CreateLeaveTypeCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool CountInPayroll { get; set; }
    }

    public class UpdateLeaveTypeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool CountInPayroll { get; set; }
    }

    public class DeleteLeaveTypeCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}