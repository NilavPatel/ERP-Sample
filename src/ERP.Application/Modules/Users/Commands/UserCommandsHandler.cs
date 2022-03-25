using ERP.Application.Core;
using ERP.Domain.Core.Models;
using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Domain.Modules.Users;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ERP.Application.Modules.Users.Commands
{
    public class RegisterUserCommandHandler : BaseCommandHandler, IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IEncryptionService _encryptionService;

        public RegisterUserCommandHandler(IMediator mediator,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IConfiguration configuration,
            IEncryptionService encryptionService,
            IUserContext userContext) : base(mediator, userContext)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _configuration = configuration;
            _encryptionService = encryptionService;
        }

        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var saltKey = _encryptionService.CreateSaltKey(5);
            var passwordHash = _encryptionService.CreatePasswordHash(request.Password, saltKey);

            var newUser = User.Create(request.EmployeeId, passwordHash, saltKey, request.RoleId,
                GetCurrentEmployeeId(), IsUserExist);

            await _unitOfWork.Repository<User>().AddAsync(newUser);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.LoadRelatedEntity(newUser, x => x.Employee);

            await SendEmail(newUser, request.Password);

            return newUser.Id;
        }

        private async Task<bool> IsUserExist(Guid employeeId)
        {
            var spec = UserSpecifications.GetUserByEmployeeIdSpec(employeeId);
            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(spec, false);
            return user != null;
        }

        private async Task SendEmail(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(user.Employee?.OfficeEmailId))
            {
                return;
            }
            var rootPath = _configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var templatePath = Path.Join(rootPath, "EmailTemplates\\User_Registration.html");
            var bodyTemplate = "";
            using (StreamReader SourceReader = System.IO.File.OpenText(templatePath))
            {
                bodyTemplate = SourceReader.ReadToEnd();
            }
            bodyTemplate = bodyTemplate.Replace("##UserName##", user.Employee.GetFullName());
            bodyTemplate = bodyTemplate.Replace("##EmaployeeCode##", user.Employee.EmployeeCode);
            bodyTemplate = bodyTemplate.Replace("##Password##", password);

            var mail = new Email()
            {
                From = "admin@derp.com",
                To = user.Employee.OfficeEmailId,
                Subject = "User Registration",
                Body = bodyTemplate
            };
            await _emailService.SendEmailAsync(mail);
        }

    }

    public class UpdateUserCommandHandler : BaseCommandHandler, IRequestHandler<UpdateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateUserCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
            : base(mediator, userContext)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            user.UpdateUser(request.RoleId, GetCurrentEmployeeId());

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }

    public class ResetUserPasswordCommandHandler : BaseCommandHandler, IRequestHandler<ResetUserPasswordCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IEncryptionService _encryptionService;

        public ResetUserPasswordCommandHandler(IMediator mediator,
            IUnitOfWork unitOfWork,
            IEmailService emailService,
            IConfiguration configuration,
            IEncryptionService encryptionService,
            IUserContext userContext) : base(mediator, userContext)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _configuration = configuration;
            _encryptionService = encryptionService;
        }

        public async Task<Guid> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            spec.AddInclude(x => x.Employee);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            var saltKey = _encryptionService.CreateSaltKey(5);
            var passwordHash = _encryptionService.CreatePasswordHash(request.Password, saltKey);

            user.ResetUserPassword(passwordHash, saltKey, GetCurrentEmployeeId());

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            await SendEmail(user, request.Password);

            return user.Id;
        }

        private async Task SendEmail(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(user.Employee?.OfficeEmailId))
            {
                return;
            }
            var rootPath = _configuration.GetValue<string>(WebHostDefaults.ContentRootKey);
            var templatePath = Path.Join(rootPath, "EmailTemplates\\User_Password_Reset.html");
            var bodyTemplate = "";
            using (StreamReader SourceReader = System.IO.File.OpenText(templatePath))
            {
                bodyTemplate = SourceReader.ReadToEnd();
            }
            bodyTemplate = bodyTemplate.Replace("##UserName##", user.Employee.GetFullName());
            bodyTemplate = bodyTemplate.Replace("##EmaployeeCode##", user.Employee.EmployeeCode);
            bodyTemplate = bodyTemplate.Replace("##Password##", password);

            var mail = new Email()
            {
                From = "admin@derp.com",
                To = user.Employee.OfficeEmailId,
                Subject = "Password Reset",
                Body = bodyTemplate
            };
            await _emailService.SendEmailAsync(mail);
        }

    }

    public class BlockUserCommandHandler : BaseCommandHandler, IRequestHandler<BlockUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlockUserCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
            : base(mediator, userContext)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);
            user.BlockUser(GetCurrentEmployeeId());

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }

    public class ActivateUserCommandHandler : BaseCommandHandler, IRequestHandler<ActivateUserCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ActivateUserCommandHandler(
            IMediator mediator,
            IUnitOfWork unitOfWork,
            IUserContext userContext)
            : base(mediator, userContext)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            user.ActivateUser(GetCurrentEmployeeId());

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }

    public class InvalidLoginAttemptedEventCommandHandler : IRequestHandler<InvalidLoginAttemptedCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public InvalidLoginAttemptedEventCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(InvalidLoginAttemptedCommand notification, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(notification.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            user.InvalidLoginAttempt();

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }

    public class LoginSuccessEventCommandHandler : IRequestHandler<LoginSuccessCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginSuccessEventCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(LoginSuccessCommand notification, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(notification.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            user.LoginSuccessfully();

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }
    }

}