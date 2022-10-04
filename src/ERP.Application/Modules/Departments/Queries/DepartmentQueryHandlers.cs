using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Modules.Departments;
using MediatR;

namespace ERP.Application.Modules.Departments.Queries
{
    public class DepartmentQueryHandlers :
        IRequestHandler<GetAllDepartmentsReq, GetAllDepartmentsRes>,
        IRequestHandler<GetDepartmentByIdReq, Department>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentQueryHandlers(IUnitOfWork unitOfWork)
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

            if (request.PageSize > 0)
            {
                spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            }
            var data = await _unitOfWork.Repository<Department>().ListAsync(spec, false);

            return new GetAllDepartmentsRes
            {
                Result = data,
                Count = count
            };
        }

        public async Task<Department> Handle(GetDepartmentByIdReq request, CancellationToken cancellationToken)
        {
            var spec = DepartmentSpecifications.GetDepartmentByIdSpec(request.Id);
            return await _unitOfWork.Repository<Department>().SingleAsync(spec, false);
        }
    }
}