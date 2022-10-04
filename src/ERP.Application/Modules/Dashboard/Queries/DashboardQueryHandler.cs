using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Modules.Employees;
using MediatR;

namespace ERP.Application.Modules.Dashboard.Queries
{
    public class DashboardQueryHandler : IRequestHandler<GetWeeklyBirthdaysReq, GetWeeklyBirthdaysRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DashboardQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<GetWeeklyBirthdaysRes> Handle(GetWeeklyBirthdaysReq request, CancellationToken cancellationToken)
        {
            var dateFrom = DateTimeOffset.UtcNow.Date;
            var dateTo = DateTimeOffset.UtcNow.Date.AddDays(7);

            BaseSpecification<Employee> spec = new BaseSpecification<Employee>(x =>
                x.EmployeePersonalDetail.BirthDate != null
                && x.EmployeePersonalDetail.BirthDate.Value.AddYears(dateFrom.Year - x.EmployeePersonalDetail.BirthDate.Value.Year) >= dateFrom
                && x.EmployeePersonalDetail.BirthDate.Value.AddYears(dateTo.Year - x.EmployeePersonalDetail.BirthDate.Value.Year) <= dateTo);
            spec.AddInclude(x => x.EmployeePersonalDetail);
            spec.ApplyOrderBy(x => x.EmployeePersonalDetail.BirthDate.Value.AddYears(dateFrom.Year - x.EmployeePersonalDetail.BirthDate.Value.Year));

            var data = await _unitOfWork.Repository<Employee>().ListAsync(spec, false);
            return new GetWeeklyBirthdaysRes
            {
                EmployeeBirthdays = data.Select(x => new EmployeeBirthday
                {
                    Name = x.GetNameWithDesignation(),
                    BirthDay = x.EmployeePersonalDetail.BirthDate
                })
            };
        }
    }
}