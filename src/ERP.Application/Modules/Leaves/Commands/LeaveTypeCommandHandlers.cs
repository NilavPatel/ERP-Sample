using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Leaves;
using MediatR;

namespace ERP.Application.Modules.Leaves.Commands
{
    public class CreateLeaveTypeCommandHandler : BaseCommandHandler, IRequestHandler<CreateLeaveTypeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateLeaveTypeCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var newLeaveType = LeaveType.Create(request.Name, request.Description, request.IsActive, request.CountInPayroll, GetCurrentEmployeeId(), IsLeaveTypeNameExist);

            await _unitOfWork.Repository<LeaveType>().AddAsync(newLeaveType);
            await _unitOfWork.SaveChangesAsync();

            return newLeaveType.Id;
        }

        public async Task<bool> IsLeaveTypeNameExist(string name)
        {
            var spec = LeaveTypeSpecifications.GetByLeaveTypeNameSpec(name);
            var leaveTypes = await _unitOfWork.Repository<LeaveType>().ListAsync(spec, false);
            if (leaveTypes.Any())
            {
                return true;
            }
            return false;
        }
    }

    public class UpdateLeaveTypeCommandHandler : BaseCommandHandler, IRequestHandler<UpdateLeaveTypeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLeaveTypeCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = LeaveTypeSpecifications.GetLeaveTypeByIdSpec(request.Id);
            var existingLeaveType = await _unitOfWork.Repository<LeaveType>().SingleAsync(byIdSpec, true);

            existingLeaveType.UpdateLeaveType(request.Name, request.Description, request.IsActive, request.CountInPayroll, GetCurrentEmployeeId(), IsLeaveTypeNameExist);

            _unitOfWork.Repository<LeaveType>().Update(existingLeaveType);
            await _unitOfWork.SaveChangesAsync();

            return existingLeaveType.Id;
        }

        public async Task<bool> IsLeaveTypeNameExist(Guid id, string name)
        {
            var spec = LeaveTypeSpecifications.GetByLeaveTypeNameSpec(name);
            var leaveTypes = await _unitOfWork.Repository<LeaveType>().ListAsync(spec, false);
            if (leaveTypes.Any(x => x.Id != id))
            {
                return true;
            }
            return false;
        }
    }

    public class DeleteLeaveTypeCommandHandler : BaseCommandHandler, IRequestHandler<DeleteLeaveTypeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveTypeCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = LeaveTypeSpecifications.GetLeaveTypeByIdSpec(request.Id);
            var existingLeaveType = await _unitOfWork.Repository<LeaveType>().SingleAsync(byIdSpec, true);

            existingLeaveType.DeleteLeaveType(GetCurrentEmployeeId());

            _unitOfWork.Repository<LeaveType>().Update(existingLeaveType);
            await _unitOfWork.SaveChangesAsync();

            return existingLeaveType.Id;
        }
    }
}