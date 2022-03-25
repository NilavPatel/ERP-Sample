using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles.Commands
{
    public class CreateRoleCommandHandler : BaseCommandHandler, IRequestHandler<CreateRoleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var newRole = Role.CreateRole(request.Name, request.Description, GetCurrentEmployeeId(), IsRoleNameExist);

            await _unitOfWork.Repository<Role>().AddAsync(newRole);
            await _unitOfWork.SaveChangesAsync();

            return newRole.Id;
        }

        public async Task<bool> IsRoleNameExist(string name)
        {
            var spec = RoleSpecifications.GetByRoleNameSpec(name);
            var roles = await _unitOfWork.Repository<Role>().ListAsync(spec, false);
            if (roles.Any())
            {
                return true;
            }
            return false;
        }
    }

    public class UpdateRoleCommandHandler : BaseCommandHandler, IRequestHandler<UpdateRoleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = RoleSpecifications.GetRoleByIdSpec(request.Id);
            var existingRole = await _unitOfWork.Repository<Role>().SingleAsync(byIdSpec, true);

            existingRole.UpdateRole(request.Name, request.Description, GetCurrentEmployeeId(), IsRoleNameExist);

            _unitOfWork.Repository<Role>().Update(existingRole);
            await _unitOfWork.SaveChangesAsync();

            return existingRole.Id;
        }

        public async Task<bool> IsRoleNameExist(Guid id, string name)
        {
            var spec = RoleSpecifications.GetByRoleNameSpec(name);
            var roles = await _unitOfWork.Repository<Role>().ListAsync(spec, false);
            if (roles.Any(x => x.Id != id))
            {
                return true;
            }
            return false;
        }
    }

    public class DeleteRoleCommandHandler : BaseCommandHandler, IRequestHandler<DeleteRoleCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoleCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = RoleSpecifications.GetRoleByIdSpec(request.Id);
            var existingRole = await _unitOfWork.Repository<Role>().SingleAsync(byIdSpec, true);

            existingRole.DeleteRole(GetCurrentEmployeeId());

            _unitOfWork.Repository<Role>().Update(existingRole);
            await _unitOfWork.SaveChangesAsync();

            return existingRole.Id;
        }
    }
}