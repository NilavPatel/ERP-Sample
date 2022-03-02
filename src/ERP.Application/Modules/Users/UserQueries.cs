using ERP.Domain.Enums;
using MediatR;

namespace ERP.Application.Modules.Users
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
        public DateTime? LastLogInOn { get; set; }
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
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class GetAllUsersReq : IRequest<GetAllUsersRes>
    {
        public string SearchKeyword { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetAllUsersRes
    {
        public IEnumerable<UserViewModel> Result { get; set; }
        public int Count { get; set; }
    }

    public class GetUserById : IRequest<UserViewModel>
    {
        public Guid Id { get; set; }
    }
}