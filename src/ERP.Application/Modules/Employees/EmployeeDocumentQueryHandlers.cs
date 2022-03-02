using ERP.Application.Core.Repositories;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees
{
    public class GetEmployeeDocumentsQueryHandler : IRequestHandler<GetEmployeeDocumentsReq, IList<EmployeeDocument>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeDocumentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<EmployeeDocument>> Handle(GetEmployeeDocumentsReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeDocumentSpecifications.GetEmployeeDocumentByEmployeeId(request.EmployeeId);
            return await _unitOfWork.Repository<EmployeeDocument>().ListAsync(spec, false);
        }
    }

    public class DownloadEmployeeDocumentQueryHandler : IRequestHandler<DownloadEmployeeDocumentReq, EmployeeDocument>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DownloadEmployeeDocumentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeDocument> Handle(DownloadEmployeeDocumentReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeDocumentSpecifications.GetEmployeeDocumentById(request.DocumentId);
            return await _unitOfWork.Repository<EmployeeDocument>().FirstOrDefaultAsync(spec, false);
        }
    }

}