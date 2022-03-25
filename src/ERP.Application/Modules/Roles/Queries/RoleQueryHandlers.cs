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

            spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            var data = await _unitOfWork.Repository<Role>().ListAsync(spec, false);

            return new GetAllRolesRes
            {
                Result = data,
                Count = count
            };
        }
    }

    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdReq, Role>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetRoleByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Role> Handle(GetRoleByIdReq request, CancellationToken cancellationToken)
        {
            var spec = RoleSpecifications.GetRoleByIdSpec(request.Id);
            return await _unitOfWork.Repository<Role>().SingleAsync(spec, false);
        }
    }

}