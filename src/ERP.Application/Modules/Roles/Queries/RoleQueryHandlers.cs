using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Modules.Roles;
using MediatR;

namespace ERP.Application.Modules.Roles.Queries
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesReq, GetAllRolesRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllRolesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllRolesRes> Handle(GetAllRolesReq request, CancellationToken cancellationToken)
        {
            BaseSpecification<Role> spec;
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                spec = RoleSpecifications.SearchRolesSpec(request.SearchKeyword);
            }
            else
            {
                spec = RoleSpecifications.GetAllRolesSpec();
            }
            var count = await _unitOfWork.Repository<Role>().CountAsync(spec);

            if (request.PageSize > 0)
            {
                spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            }

            var data = await _unitOfWork.Repository<Role>().ListAsync(spec, false);

            return new GetAllRolesRes
            {
                Result = data.Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description
                }).ToList(),
                Count = count
            };
        }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdReq, RoleViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleViewModel> Handle(GetRoleByIdReq request, CancellationToken cancellationToken)
        {
            var spec = RoleSpecifications.GetRoleByIdSpec(request.Id);
            var role = await _unitOfWork.Repository<Role>().SingleAsync(spec, false);
            return new RoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }
    }

}