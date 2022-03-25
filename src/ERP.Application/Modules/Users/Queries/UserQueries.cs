using ERP.Application.Core.Models;
using ERP.Domain.Enums;
using MediatR;

namespace ERP.Application.Modules.Users.Queries
{
    public class LoginReq : IRequest<LoginRes>
    {
        public string EmployeeCode { get; set; }
        public string Password { get; set; }
    }

    public class LoginRes
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Token { get; set; }
        public IEnumerable<int> Permissions { get; set; }
    }


    public class UserViewModel
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public DateTimeOffset? LastLogInOn { get; set; }
        public byte InValidLogInAttemps { get; set; }
        public UserStatus Status { get; set; }
        public string StatusText { get; set; }
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string? EmailId { get; set; }
        public string? MobileNo { get; set; }
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
    }

    public class GetAllUsersReq : PagedListReq, IRequest<GetAllUsersRes>
    { }

    public class GetAllUsersRes : PagedListRes<UserViewModel>
    { }

    public class GetUserByIdReq : IRequest<UserViewModel>
    {
        public Guid Id { get; set; }
    }
}