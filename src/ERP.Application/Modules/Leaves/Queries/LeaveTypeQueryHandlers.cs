using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Modules.Leaves;
using MediatR;

namespace ERP.Application.Modules.Leaves.Queries
{
    public class GetAllLeaveTypesQueryHandler : IRequestHandler<GetAllLeaveTypesReq, GetAllLeaveTypesRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllLeaveTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetAllLeaveTypesRes> Handle(GetAllLeaveTypesReq request, CancellationToken cancellationToken)
        {
            BaseSpecification<LeaveType> spec;
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                spec = LeaveTypeSpecifications.SearchLeaveTypesSpec(request.SearchKeyword);
            }
            else
            {
                spec = LeaveTypeSpecifications.GetAllLeaveTypesSpec();
            }
            var count = await _unitOfWork.Repository<LeaveType>().CountAsync(spec);

            if (request.PageSize > 0)
            {
                spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            }
            var data = await _unitOfWork.Repository<LeaveType>().ListAsync(spec, false);

            return new GetAllLeaveTypesRes
            {
                Result = data,
                Count = count
            };
        }
    }

    public class GetLeaveTypeByIdQueryHandler : IRequestHandler<GetLeaveTypeByIdReq, LeaveType>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetLeaveTypeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<LeaveType> Handle(GetLeaveTypeByIdReq request, CancellationToken cancellationToken)
        {
            var spec = LeaveTypeSpecifications.GetLeaveTypeByIdSpec(request.Id);
            return await _unitOfWork.Repository<LeaveType>().SingleAsync(spec, false);
        }
    }

    public class GetAllActiveLeaveTypesQueryHandler : IRequestHandler<GetAllActiveLeaveTypesReq, IList<LeaveType>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllActiveLeaveTypesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<LeaveType>> Handle(GetAllActiveLeaveTypesReq request, CancellationToken cancellationToken)
        {
            var spec = LeaveTypeSpecifications.GetAllActiveLeaveTypesSpec();
            return await _unitOfWork.Repository<LeaveType>().ListAsync(spec, false);
        }
    }

}