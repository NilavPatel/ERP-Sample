using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Leaves;
using MediatR;

namespace ERP.Application.Modules.Leaves.Commands
{
    public class CreateHolidayCommandHandler : BaseCommandHandler, IRequestHandler<CreateHolidayCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateHolidayCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
        {
            var newHoliday = Holiday.Create(request.Name, request.HolidayOn, GetCurrentEmployeeId(), IsHolidayExist);

            await _unitOfWork.Repository<Holiday>().AddAsync(newHoliday);
            await _unitOfWork.SaveChangesAsync();

            return newHoliday.Id;
        }

        public async Task<bool> IsHolidayExist(DateTimeOffset holidayOn)
        {
            var spec = HolidaySpecifications.GetHolidayByDateSpec(holidayOn);
            var Holidays = await _unitOfWork.Repository<Holiday>().ListAsync(spec, false);
            if (Holidays.Any())
            {
                return true;
            }
            return false;
        }
    }

    public class UpdateHolidayCommandHandler : BaseCommandHandler, IRequestHandler<UpdateHolidayCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHolidayCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = HolidaySpecifications.GetHolidayByIdSpec(request.Id);
            var existingHoliday = await _unitOfWork.Repository<Holiday>().SingleAsync(byIdSpec, true);

            existingHoliday.UpdateHoliday(request.Name, request.HolidayOn, GetCurrentEmployeeId(), IsHolidayExist);

            _unitOfWork.Repository<Holiday>().Update(existingHoliday);
            await _unitOfWork.SaveChangesAsync();

            return existingHoliday.Id;
        }

        public async Task<bool> IsHolidayExist(Guid id, DateTimeOffset holidayOn)
        {
            var spec = HolidaySpecifications.GetHolidayByDateSpec(holidayOn);
            var Holidays = await _unitOfWork.Repository<Holiday>().ListAsync(spec, false);
            if (Holidays.Any(x => x.Id != id))
            {
                return true;
            }
            return false;
        }
    }

    public class DeleteHolidayCommandHandler : BaseCommandHandler, IRequestHandler<DeleteHolidayCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHolidayCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = HolidaySpecifications.GetHolidayByIdSpec(request.Id);
            var existingHoliday = await _unitOfWork.Repository<Holiday>().SingleAsync(byIdSpec, true);

            existingHoliday.DeleteHoliday(GetCurrentEmployeeId());

            _unitOfWork.Repository<Holiday>().Update(existingHoliday);
            await _unitOfWork.SaveChangesAsync();

            return existingHoliday.Id;
        }
    }
}