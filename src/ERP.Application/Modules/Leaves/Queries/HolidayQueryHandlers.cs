using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Modules.Leaves;
using MediatR;

namespace ERP.Application.Modules.Leaves.Queries
{
    public class GetAllHolidaysQueryHandler : IRequestHandler<GetAllHolidaysReq, GetAllHolidaysRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllHolidaysQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllHolidaysRes> Handle(GetAllHolidaysReq request, CancellationToken cancellationToken)
        {
            BaseSpecification<Holiday> spec;
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                spec = HolidaySpecifications.SearchHolidayByNameAndYearSpec(request.SearchKeyword);
            }
            else
            {
                spec = HolidaySpecifications.GetAllHolidaysSpec();
            }
            var count = await _unitOfWork.Repository<Holiday>().CountAsync(spec);

            if (request.PageSize > 0)
            {
                spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            }
            var data = await _unitOfWork.Repository<Holiday>().ListAsync(spec, false);

            return new GetAllHolidaysRes
            {
                Result = data,
                Count = count
            };
        }
    }

    public class GetHolidayByIdQueryHandler : IRequestHandler<GetHolidayByIdReq, Holiday>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetHolidayByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Holiday> Handle(GetHolidayByIdReq request, CancellationToken cancellationToken)
        {
            var spec = HolidaySpecifications.GetHolidayByIdSpec(request.Id);
            return await _unitOfWork.Repository<Holiday>().SingleAsync(spec, false);
        }
    }

    public class GetAllHolidayByYearQueryHandler : IRequestHandler<GetAllHolidayByYearReq, IList<Holiday>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllHolidayByYearQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<Holiday>> Handle(GetAllHolidayByYearReq request, CancellationToken cancellationToken)
        {
            var spec = HolidaySpecifications.GetAllHolidaysInYearSpec(request.Year);
            return await _unitOfWork.Repository<Holiday>().ListAsync(spec, false);
        }
    }
}