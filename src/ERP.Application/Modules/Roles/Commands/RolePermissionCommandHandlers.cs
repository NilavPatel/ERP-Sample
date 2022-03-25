using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles.Commands
{
    public class AddRolePermissionsCommandHandler : BaseCommandHandler, IRequestHandler<AddRolePermissionsCommnd, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddRolePermissionsCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(AddRolePermissionsCommnd request, CancellationToken cancellationToken)
        {
            var spec = RoleSpecifications.GetRoleByIdSpec(request.RoleId);
            var role = await _unitOfWork.Repository<Role>().SingleAsync(spec, false);

            var allPermissions = await _unitOfWork.Repository<Permission>().ListAllAsync(false);
            if (request.Permissions.Except(allPermissions.Select(x => x.Id)).Any())
            {
                throw new RecordNotFoundException("Permission Not Found");
            }

            // Remove old permissions
            var rolePermissionsSpec = RolePermissionSpecifications.GetByRoleIdSpec(request.RoleId);
            var oldPermissions = await _unitOfWork.Repository<RolePermission>().ListAsync(rolePermissionsSpec, true);
            foreach (var permission in oldPermissions)
            {
                permission.RemoveRolePermission(GetCurrentEmployeeId());
                _unitOfWork.Repository<RolePermission>().Update(permission);
            }

            // Add new permissions
            foreach (var permission in request.Permissions)
            {
                var rolePermission = RolePermission.CreateRolePermission(role.Id, permission, GetCurrentEmployeeId());
                await _unitOfWork.Repository<RolePermission>().AddAsync(rolePermission);
            }
            await _unitOfWork.SaveChangesAsync();

            return request.RoleId;
        }
    }
}