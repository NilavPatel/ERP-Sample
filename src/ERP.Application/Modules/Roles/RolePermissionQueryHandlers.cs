using ERP.Application.Core.Repositories;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles
{
    public class GetAllRolePermissionByRoleIdQueryHandler : IRequestHandler<GetAllRolePermissionByRoleId, IList<RolePermissionViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllRolePermissionByRoleIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<RolePermissionViewModel>> Handle(GetAllRolePermissionByRoleId request, CancellationToken cancellationToken)
        {
            var roleSpec = RoleSpecifications.GetRoleByIdSpec(request.RoleId);
            var role = await _unitOfWork.Repository<Role>().FirstOrDefaultAsync(roleSpec, false);
            if (role == null)
            {
                throw new DomainException("Role Not Found");
            }

            var spec = RolePermissionSpecifications.GetByRoleIdSpec(request.RoleId);
            var rolePermissions = await _unitOfWork.Repository<RolePermission>().ListAsync(spec, false);
            var permissions = (await _unitOfWork.Repository<Permission>().ListAllAsync(false)).Select(x => new RolePermissionViewModel
            {
                Id = x.Id,
                RoleId = role.Id,
                Name = x.Name,
                Description = x.Description,
                GroupName = x.GroupName,
                HasPermission = rolePermissions.Any(y => y.PermissionId == x.Id)
            }).ToList();

            return permissions;
        }
    }
}