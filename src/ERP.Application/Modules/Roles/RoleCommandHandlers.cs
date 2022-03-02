using ERP.Application.Core;
using ERP.Application.Core.Repositories;
using ERP.Application.Core.Services;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles
{
    public class CreateRoleCommandHandler : BaseCommandHandler, IRequestHandler<CreateRole, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateRole request, CancellationToken cancellationToken)
        {
            var newRole = Role.CreateRole(request.Name, request.Description, GetUserId(), IsRoleNameExist);

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

    public class UpdateRoleCommandHandler : BaseCommandHandler, IRequestHandler<UpdateRole, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateRole request, CancellationToken cancellationToken)
        {
            var byIdSpec = RoleSpecifications.GetRoleByIdSpec(request.Id);
            var existingRole = await _unitOfWork.Repository<Role>().FirstOrDefaultAsync(byIdSpec, true);
            if (existingRole == null)
            {
                throw new DomainException("Role Not Found");
            }

            existingRole.UpdateRole(request.Name, request.Description, GetUserId(), IsRoleNameExist);

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
}