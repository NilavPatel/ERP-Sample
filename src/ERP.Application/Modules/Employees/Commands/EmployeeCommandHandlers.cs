using ERP.Application.Core;
using MediatR;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Employees;
using ERP.Domain.Modules.Designations;
using ERP.Domain.Modules.Departments;

namespace ERP.Application.Modules.Employees.Commands
{
    public class CreateEmployeeCommandHandler : BaseCommandHandler, IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(IMediator mediator, IUserContext userContext, IUnitOfWork unitOfWork) : base(mediator, userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = Employee.Create(
                request.FirstName,
                request.MiddleName,
                request.LastName,
                request.BirthDate,
                request.Gender,
                request.ParmenantAddress,
                request.CurrentAddress,
                request.IsCurrentSameAsParmenantAddress,
                request.PersonalEmailId,
                request.PersonalMobileNo,
                request.OtherContactNo,
                request.EmployeeCode,
                request.JoiningOn,
                GetCurrentEmployeeId(),
                IsEmployeeCodeExist);

            await _unitOfWork.Repository<Employee>().AddAsync(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<bool> IsEmployeeCodeExist(string employeeCode)
        {
            var spec = EmployeeSpecifications.GetEmployeeByEmployeeCodeSpec(employeeCode);
            var employee = await _unitOfWork.Repository<Employee>().FirstOrDefaultAsync(spec, false);
            return employee != null;
        }
    }

    public class UpdateEmployeePersonalDetailsCommandHandler : BaseCommandHandler, IRequestHandler<UpdateEmployeePersonalDetailsCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeePersonalDetailsCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext userContext) : base(mediator, userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateEmployeePersonalDetailsCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(request.Id);
            var employee = await _unitOfWork.Repository<Employee>().SingleAsync(spec, true);

            employee.UpdateEmployeePersonalDetails(
                 request.FirstName,
                 request.MiddleName,
                 request.LastName,
                 request.BirthDate,
                 request.BloodGroup,
                 request.Gender,
                 request.ParmenantAddress,
                 request.CurrentAddress,
                 request.IsCurrentSameAsParmenantAddress,
                 request.MaritalStatus,
                 request.PersonalEmailId,
                 request.PersonalMobileNo,
                 request.OtherContactNo,
                 GetCurrentEmployeeId()
                 );

            _unitOfWork.Repository<Employee>().Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee.Id;
        }
    }

    public class UpdateEmployeeOfficeDetailsCommandHandler : BaseCommandHandler, IRequestHandler<UpdateEmployeeOfficeDetailsCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeOfficeDetailsCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext userContext)
            : base(mediator, userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateEmployeeOfficeDetailsCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(request.Id);
            var employee = await _unitOfWork.Repository<Employee>().SingleAsync(spec, true);

            employee.UpdateEmployeeOfficeDetails(
                request.OfficeEmailId,
                request.OfficeContactNo,
                request.JoiningOn,
                request.RelievingOn,
                request.DesignationId,
                request.ReportingToId,
                request.DepartmentId,
                GetCurrentEmployeeId(),
                IsDesignationExist,
                IsReportingToExist,
                IsDepartmentExist);

            _unitOfWork.Repository<Employee>().Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee.Id;
        }

        public async Task<bool> IsDesignationExist(Guid designationId)
        {
            var spec = DesignationSpecifications.GetDesignationByIdSpec(designationId);
            var designation = await _unitOfWork.Repository<Designation>().FirstOrDefaultAsync(spec, false);
            return designation != null;
        }

        public async Task<bool> IsReportingToExist(Guid employeeId)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(employeeId);
            var employee = await _unitOfWork.Repository<Employee>().FirstOrDefaultAsync(spec, false);
            return employee != null;
        }

        public async Task<bool> IsDepartmentExist(Guid departmentId)
        {
            var spec = DepartmentSpecifications.GetDepartmentByIdSpec(departmentId);
            var department = await _unitOfWork.Repository<Department>().FirstOrDefaultAsync(spec, false);
            return department != null;
        }
    }

    public class UploadEmployeeProfilePhotoCommandHandler : BaseCommandHandler, IRequestHandler<UploadEmployeeProfilePhotoCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public UploadEmployeeProfilePhotoCommandHandler(IMediator mediator, IUnitOfWork unitOfWork,
         IUserContext userContext, IFileService fileService) : base(mediator, userContext)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }

        public async Task<Guid> Handle(UploadEmployeeProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(request.Id);
            var employee = await _unitOfWork.Repository<Employee>().SingleAsync(spec, true);

            if (request.Photo == null)
            {
                throw new Exception("Document Not Found");
            }

            var id = Guid.NewGuid();
            await _fileService.UploadFile(request.Photo, id);

            string ext = Path.GetExtension(request.Photo.FileName);
            var fileName = id + ext;
            employee.UploadProfilePhoto(fileName, GetCurrentEmployeeId());

            _unitOfWork.Repository<Employee>().Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee.Id;
        }
    }

    public class RemoveEmployeeProfilePhotoCommandHandler : BaseCommandHandler, IRequestHandler<RemoveEmployeeProfilePhotoCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RemoveEmployeeProfilePhotoCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, IUserContext userContext)
             : base(mediator, userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(RemoveEmployeeProfilePhotoCommand request, CancellationToken cancellationToken)
        {
            var spec = EmployeeSpecifications.GetEmployeeByIdSpec(request.Id);
            var employee = await _unitOfWork.Repository<Employee>().SingleAsync(spec, true);

            employee.UploadProfilePhoto(null, GetCurrentEmployeeId());

            _unitOfWork.Repository<Employee>().Update(employee);
            await _unitOfWork.SaveChangesAsync();

            return employee.Id;
        }
    }

}