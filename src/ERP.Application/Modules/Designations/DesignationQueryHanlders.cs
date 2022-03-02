using ERP.Application.Core.Repositories;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Designations;
using MediatR;

namespace ERP.Application.Modules.Designations
{
    public class GetAllDesignationsQueryHandler : IRequestHandler<GetAllDesignationsReq, GetAllDesignationsRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllDesignationsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllDesignationsRes> Handle(GetAllDesignationsReq request, CancellationToken cancellationToken)
        {
            BaseSpecification<Designation> spec;
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                spec = DesignationSpecifications.SearchDesignationsSpec(request.SearchKeyword);
            }
            else
            {
                spec = DesignationSpecifications.GetAllDesignationsSpec();
            }
            var count = await _unitOfWork.Repository<Designation>().CountAsync(spec);

            spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            var data = await _unitOfWork.Repository<Designation>().ListAsync(spec, false);

            return new GetAllDesignationsRes
            {
                Result = data,
                Count = count
            };
        }
    }

    public class GetDesignationByIdQueryHandler : IRequestHandler<GetDesignationById, Designation>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetDesignationByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Designation> Handle(GetDesignationById request, CancellationToken cancellationToken)
        {
            var spec = DesignationSpecifications.GetDesignationByIdSpec(request.Id);
            var designation = await _unitOfWork.Repository<Designation>().FirstOrDefaultAsync(spec, false);
            if (designation == null)
            {
                throw new RecordNotFoundException("Designation Not Found.");
            }
            return designation;
        }
    }

}