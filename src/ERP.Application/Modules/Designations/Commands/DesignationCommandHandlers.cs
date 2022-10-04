using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Designations;
using MediatR;

namespace ERP.Application.Modules.Designations.Commands
{
    public class DesignationCommandHandlers : BaseCommandHandler,
        IRequestHandler<CreateDesignationCommand, Guid>,
        IRequestHandler<UpdateDesignationCommand, Guid>,
        IRequestHandler<DeleteDesignationCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DesignationCommandHandlers(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateDesignationCommand request, CancellationToken cancellationToken)
        {
            var newDesignation = Designation.CreateDesignation(request.Name, request.Description, GetCurrentEmployeeId(),
             IsDesignationNameExist);

            await _unitOfWork.Repository<Designation>().AddAsync(newDesignation);
            await _unitOfWork.SaveChangesAsync();

            return newDesignation.Id;
        }

        private async Task<bool> IsDesignationNameExist(string name)
        {
            var spec = DesignationSpecifications.GetByDesignationNameSpec(name);
            var designations = await _unitOfWork.Repository<Designation>().ListAsync(spec, false);
            if (designations.Any())
            {
                return true;
            }
            return false;
        }

        public async Task<Guid> Handle(UpdateDesignationCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = DesignationSpecifications.GetDesignationByIdSpec(request.Id);
            var existingDesignation = await _unitOfWork.Repository<Designation>().SingleAsync(byIdSpec, true);

            existingDesignation.UpdateDesignation(request.Name, request.Description, GetCurrentEmployeeId(), IsDesignationNameExist);

            _unitOfWork.Repository<Designation>().Update(existingDesignation);
            await _unitOfWork.SaveChangesAsync();

            return existingDesignation.Id;
        }

        private async Task<bool> IsDesignationNameExist(Guid id, string name)
        {
            var spec = DesignationSpecifications.GetByDesignationNameSpec(name);
            var designations = await _unitOfWork.Repository<Designation>().ListAsync(spec, false);
            if (designations.Any(x => x.Id != id))
            {
                return true;
            }
            return false;
        }

        public async Task<Guid> Handle(DeleteDesignationCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = DesignationSpecifications.GetDesignationByIdSpec(request.Id);
            var existingDesignation = await _unitOfWork.Repository<Designation>().SingleAsync(byIdSpec, true);

            existingDesignation.DeleteDesignation(GetCurrentEmployeeId());

            _unitOfWork.Repository<Designation>().Update(existingDesignation);
            await _unitOfWork.SaveChangesAsync();

            return existingDesignation.Id;
        }
    }
}