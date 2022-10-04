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
    public class UserCommandsHandler : BaseCommandHandler,
        IRequestHandler<RegisterUserCommand, Guid>,
        IRequestHandler<UpdateUserCommand, Guid>,
        IRequestHandler<ResetUserPasswordCommand, Guid>,
        IRequestHandler<BlockUserCommand, Guid>,
        IRequestHandler<ActivateUserCommand, Guid>,
        IRequestHandler<InvalidLoginAttemptedCommand, Guid>,
        IRequestHandler<LoginSuccessCommand, Guid>,
        IRequestHandler<SetRefreshTokenCommand, Guid>,
        IRequestHandler<RevokeRefreshTokenCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IEncryptionService _encryptionService;

        public UserCommandsHandler(IMediator mediator,
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

            var newUser = User.Create(request.EmployeeId, passwordHash, saltKey,
                GetCurrentEmployeeId(), IsUserExist);

            await _unitOfWork.Repository<User>().AddAsync(newUser);
            await _unitOfWork.LoadRelatedEntity(newUser, x => x.Employee);

            foreach (var roleId in request.RoleIds)
            {
                var userRole = UserRole.Create(newUser.Id, roleId, GetCurrentEmployeeId());
                await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
            }

            await _unitOfWork.SaveChangesAsync();

            await SendEmailForRegisterUser(newUser, request.Password);

            return newUser.Id;
        }

        private async Task<bool> IsUserExist(Guid employeeId)
        {
            var spec = UserSpecifications.GetUserByEmployeeIdSpec(employeeId);
            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(spec, false);
            return user != null;
        }

        private async Task SendEmailForRegisterUser(User user, string password)
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
            bodyTemplate = bodyTemplate.Replace("##UserName##", user.Employee.GetNameWithDesignation());
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

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            // remove all roles
            var userRoleSpec = UserRoleSpecifications.GetByUserId(user.Id);
            var userRoles = await _unitOfWork.Repository<UserRole>().ListAsync(userRoleSpec, true);
            foreach (var userRole in userRoles)
            {
                userRole.Remove(GetCurrentEmployeeId());
                _unitOfWork.Repository<UserRole>().Update(userRole);
            }

            // add new roles
            foreach (var roleId in request.RoleIds)
            {
                var userRole = UserRole.Create(user.Id, roleId, GetCurrentEmployeeId());
                await _unitOfWork.Repository<UserRole>().AddAsync(userRole);
            }

            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }

        public async Task<Guid> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            var saltKey = _encryptionService.CreateSaltKey(5);
            var passwordHash = _encryptionService.CreatePasswordHash(request.Password, saltKey);

            user.ResetUserPassword(passwordHash, saltKey, GetCurrentEmployeeId());

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            await SendEmailForResetPassword(user, request.Password);

            return user.Id;
        }

        private async Task SendEmailForResetPassword(User user, string password)
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
            bodyTemplate = bodyTemplate.Replace("##UserName##", user.Employee.GetNameWithDesignation());
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

        public async Task<Guid> Handle(BlockUserCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);
            user.BlockUser(GetCurrentEmployeeId());

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
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

        public async Task<Guid> Handle(InvalidLoginAttemptedCommand notification, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(notification.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            user.InvalidLoginAttempt();

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
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

        public async Task<Guid> Handle(SetRefreshTokenCommand notification, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(notification.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            user.SetRefreshToken(notification.Token);

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }

        public async Task<Guid> Handle(RevokeRefreshTokenCommand notification, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(notification.Id);
            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, true);

            user.RevokeRefreshToken(GetCurrentEmployeeId());

            _unitOfWork.Repository<User>().Update(user);
            await _unitOfWork.SaveChangesAsync();

            return user.Id;
        }

    }
}