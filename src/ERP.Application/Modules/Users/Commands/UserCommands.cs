using MediatR;

namespace ERP.Application.Modules.Users.Commands
{
    public class RegisterUserCommand : IRequest<Guid>
    {
        public Guid EmployeeId { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UpdateUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
    }

    public class ResetUserPasswordCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }

    public class BlockUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class ActivateUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class InvalidLoginAttemptedCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }

    public class LoginSuccessCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}