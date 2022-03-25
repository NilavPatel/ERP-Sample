using ERP.Domain.Core.Repositories;
using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles.Queries
{
    public class GetAllRolePermissionByRoleIdQueryHandler : IRequestHandler<GetAllRolePermissionByRoleIdReq, IList<RolePermissionViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllRolePermissionByRoleIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<RolePermissionViewModel>> Handle(GetAllRolePermissionByRoleIdReq request, CancellationToken cancellationToken)
        {
            var roleSpec = RoleSpecifications.GetRoleByIdSpec(request.RoleId);
            var role = await _unitOfWork.Repository<Role>().SingleAsync(roleSpec, false);

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