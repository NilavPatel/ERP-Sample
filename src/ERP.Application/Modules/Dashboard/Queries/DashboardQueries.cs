using MediatR;

namespace ERP.Application.Modules.Dashboard.Queries
{
    public class GetWeeklyBirthdaysReq : IRequest<GetWeeklyBirthdaysRes>
    { }

    public class GetWeeklyBirthdaysRes
    {
        public IEnumerable<EmployeeBirthday> EmployeeBirthdays { get; set; }
    }

    public class EmployeeBirthday
    {
        public string Name { get; set; }
        public DateTimeOffset? BirthDay { get; set; }
    }
}