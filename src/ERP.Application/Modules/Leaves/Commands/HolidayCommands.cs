using MediatR;

namespace ERP.Application.Modules.Leaves.Commands
{
    public class CreateHolidayCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public DateTimeOffset HolidayOn { get; set; }
    }

    public class UpdateHolidayCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset HolidayOn { get; set; }
    }

    public class DeleteHolidayCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}