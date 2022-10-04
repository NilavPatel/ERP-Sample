using ERP.Application.Core;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Departments;
using MediatR;

namespace ERP.Application.Modules.Departments.Commands
{
    public class DepartmentCommandHandlers : BaseCommandHandler,
        IRequestHandler<CreateDepartmentCommand, Guid>,
        IRequestHandler<UpdateDepartmentCommand, Guid>,
        IRequestHandler<DeleteDepartmentCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentCommandHandlers(IMediator mediator, IUnitOfWork unitOfWork, IUserContext _userContext) : base(mediator, _userContext)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var newDepartment = Department.CreateDepartment(request.Name, request.Description, GetCurrentEmployeeId(),
             IsDepartmentNameExist);

            await _unitOfWork.Repository<Department>().AddAsync(newDepartment);
            await _unitOfWork.SaveChangesAsync();

            return newDepartment.Id;
        }

        private async Task<bool> IsDepartmentNameExist(string name)
        {
            var spec = DepartmentSpecifications.GetByDepartmentNameSpec(name);
            var Departments = await _unitOfWork.Repository<Department>().ListAsync(spec, false);
            return Departments.Any();
        }

        public async Task<Guid> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = DepartmentSpecifications.GetDepartmentByIdSpec(request.Id);
            var existingDepartment = await _unitOfWork.Repository<Department>().SingleAsync(byIdSpec, true);

            existingDepartment.UpdateDepartment(request.Name, request.Description, GetCurrentEmployeeId(), IsDepartmentNameExist);

            _unitOfWork.Repository<Department>().Update(existingDepartment);
            await _unitOfWork.SaveChangesAsync();

            return existingDepartment.Id;
        }

        private async Task<bool> IsDepartmentNameExist(Guid id, string name)
        {
            var spec = DepartmentSpecifications.GetByDepartmentNameSpec(name);
            var Departments = await _unitOfWork.Repository<Department>().ListAsync(spec, false);
            return Departments.Any(x => x.Id != id);
        }

        public async Task<Guid> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            var byIdSpec = DepartmentSpecifications.GetDepartmentByIdSpec(request.Id);
            var existingDepartment = await _unitOfWork.Repository<Department>().SingleAsync(byIdSpec, true);

            existingDepartment.DeleteDepartment(GetCurrentEmployeeId());

            _unitOfWork.Repository<Department>().Update(existingDepartment);
            await _unitOfWork.SaveChangesAsync();

            return existingDepartment.Id;
        }
    }
}