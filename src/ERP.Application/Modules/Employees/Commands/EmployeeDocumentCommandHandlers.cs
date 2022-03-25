using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Employees.Commands
{
    public class UploadEmployeeDocumentCommandHandler : BaseCommandHandler, IRequestHandler<UploadEmployeeDocumentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UploadEmployeeDocumentCommandHandler(IMediator mediator, IUnitOfWork unitOfWork,
         IUserContext _userContext, IFileService fileService) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Guid> Handle(UploadEmployeeDocumentCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(request.EmployeeId);
            var employee = await _unitOfWork.Repository<Employee>().SingleAsync(spec, false);
            if (request.Document == null)
            {
                throw new ArgumentNullException("File Not Found");
            }

            var id = Guid.NewGuid();
            await _fileService.UploadFile(request.Document, id);

            var document = EmployeeDocument.UploadDocument(
                id,
                request.EmployeeId,
                request.Document.FileName,
                request.Description,
                GetCurrentEmployeeId());

            await _unitOfWork.Repository<EmployeeDocument>().AddAsync(document);
            await _unitOfWork.SaveChangesAsync();

            return id;
        }
    }

    public class RemoveEmployeeDocumentCommandHandler : BaseCommandHandler, IRequestHandler<RemoveEmployeeDocumentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RemoveEmployeeDocumentCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RemoveEmployeeDocumentCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeDocumentSpecifications.GetEmployeeDocumentByIdSpec(request.Id);
            var employeeDocument = await _unitOfWork.Repository<EmployeeDocument>().SingleAsync(spec, true);

            employeeDocument.RemoveDocument(GetCurrentEmployeeId());

            _unitOfWork.Repository<EmployeeDocument>().Update(employeeDocument);
            await _unitOfWork.SaveChangesAsync();

            return request.Id;
        }
    }
}