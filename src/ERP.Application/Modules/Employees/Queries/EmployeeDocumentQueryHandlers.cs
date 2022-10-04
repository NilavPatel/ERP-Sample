using ERP.Domain.Core.Repositories;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Queries
{
    public class GetEmployeeDocumentsQueryHandler : IRequestHandler<GetEmployeeDocumentsReq, IList<EmployeeDocumentViewModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetEmployeeDocumentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<EmployeeDocumentViewModel>> Handle(GetEmployeeDocumentsReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeDocumentSpecifications.GetEmployeeDocumentByEmployeeIdSpec(request.EmployeeId);
            var data = await _unitOfWork.Repository<EmployeeDocument>().ListAsync(spec, false);
            return data.Select(x => new EmployeeDocumentViewModel
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                FileName = x.FileName,
                Description = x.Description
            }).ToList();
        }
    }

    public class DownloadEmployeeDocumentQueryHandler : IRequestHandler<DownloadEmployeeDocumentReq, EmployeeDocumentViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DownloadEmployeeDocumentQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeDocumentViewModel> Handle(DownloadEmployeeDocumentReq request, CancellationToken cancellationToken)
        {
            var spec = EmployeeDocumentSpecifications.GetEmployeeDocumentByIdSpec(request.DocumentId);
            var document = await _unitOfWork.Repository<EmployeeDocument>().SingleAsync(spec, false);
            return new EmployeeDocumentViewModel
            {
                Id = document.Id,
                EmployeeId = document.EmployeeId,
                FileName = document.FileName,
                Description = document.Description
            };
        }
    }

}