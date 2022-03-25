using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Modules.Departments;
using MediatR;

namespace ERP.Application.Modules.Departments.Queries
{
    public class GetAllDepartmentsQueryHandler : IRequestHandler<GetAllDepartmentsReq, GetAllDepartmentsRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllDepartmentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllDepartmentsRes> Handle(GetAllDepartmentsReq request, CancellationToken cancellationToken)
        {
            BaseSpecification<Department> spec;
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                spec = DepartmentSpecifications.SearchDepartmentsSpec(request.SearchKeyword);
            }
            else
            {
                spec = DepartmentSpecifications.GetAllDepartmentsSpec();
            }
            var count = await _unitOfWork.Repository<Department>().CountAsync(spec);

            spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            var data = await _unitOfWork.Repository<Department>().ListAsync(spec, false);

            return new GetAllDepartmentsRes
            {
                Result = data,
                Count = count
            };
        }
    }

    public class GetDepartmentByIdQueryHandler : IRequestHandler<GetDepartmentByIdReq, Department>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDepartmentByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Department> Handle(GetDepartmentByIdReq request, CancellationToken cancellationToken)
        {
            var spec = DepartmentSpecifications.GetDepartmentByIdSpec(request.Id);
            return await _unitOfWork.Repository<Department>().SingleAsync(spec, false);
        }
    }

}