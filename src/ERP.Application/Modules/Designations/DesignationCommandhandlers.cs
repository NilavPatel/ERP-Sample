using ERP.Application.Core;
using ERP.Application.Core.Repositories;
using ERP.Application.Core.Services;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Designations;
using MediatR;

namespace ERP.Application.Modules.Designations
{
    public class CreateDesignationCommandHandler : BaseCommandHandler, IRequestHandler<CreateDesignation, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateDesignationCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateDesignation request, CancellationToken cancellationToken)
        {
            var newDesignation = Designation.CreateDesignation(request.Name, request.Description, GetUserId(),
             IsDesignationNameExist);

            await _unitOfWork.Repository<Designation>().AddAsync(newDesignation);
            await _unitOfWork.SaveChangesAsync();

            return newDesignation.Id;
        }

        public async Task<bool> IsDesignationNameExist(string name)
        {
            var spec = DesignationSpecifications.GetByDesignationNameSpec(name);
            var designations = await _unitOfWork.Repository<Designation>().ListAsync(spec, false);
            if (designations.Any())
            {
                return true;
            }
            return false;
        }
    }

    public class UpdateDesignationCommandHandler : BaseCommandHandler, IRequestHandler<UpdateDesignation, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateDesignationCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateDesignation request, CancellationToken cancellationToken)
        {
            var byIdSpec = DesignationSpecifications.GetDesignationByIdSpec(request.Id);
            var existingDesignation = await _unitOfWork.Repository<Designation>().FirstOrDefaultAsync(byIdSpec, true);
            if (existingDesignation == null)
            {
                throw new DomainException("Designation Not Found");
            }

            existingDesignation.UpdateDesignation(request.Name, request.Description, GetUserId(), IsDesignationNameExist);

            _unitOfWork.Repository<Designation>().Update(existingDesignation);
            await _unitOfWork.SaveChangesAsync();

            return existingDesignation.Id;
        }

        public async Task<bool> IsDesignationNameExist(Guid id, string name)
        {
            var spec = DesignationSpecifications.GetByDesignationNameSpec(name);
            var designations = await _unitOfWork.Repository<Designation>().ListAsync(spec, false);
            if (designations.Any(x => x.Id != id))
            {
                return true;
            }
            return false;
        }
    }
}