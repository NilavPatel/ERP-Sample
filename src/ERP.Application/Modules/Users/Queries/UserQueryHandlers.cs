using ERP.Domain.Core.Repositories;
using ERP.Domain.Core.Services;
using ERP.Application.Modules.Users.Commands;
using ERP.Application.Core.Helpers;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Roles;
using ERP.Domain.Modules.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using ERP.Domain.Core.Helpers;
using System.Security.Claims;

namespace ERP.Application.Modules.Users.Queries
{
    public class UserQueryHandlers :
        IRequestHandler<LoginReq, LoginRes>,
        IRequestHandler<GetAllUsersReq, GetAllUsersRes>,
        IRequestHandler<GetUserByIdReq, UserViewModel>,
        IRequestHandler<ValidateRefreshTokenReq, ValidateRefreshTokenRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        public UserQueryHandlers(IUnitOfWork unitOfWork,
            IEncryptionService encryptionService,
            IConfiguration configuration,
            IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
            _config = configuration;
            _mediator = mediator;
        }

        public async Task<LoginRes> Handle(LoginReq request, CancellationToken cancellationToken)
        {
            var userSpec = UserSpecifications.GetUserByEmployeeCodeSpec(request.EmployeeCode);
            userSpec.AddInclude(x => x.UserRoles);
            var user = await _unitOfWork.Repository<User>().SingleAsync(userSpec, false);
            if (user.Employee == null)
            {
                throw new DomainException("Invalid Credentials");
            }
            var passwordHash = _encryptionService.CreatePasswordHash(request.Password, user.SaltKey);
            if (user.PasswordHash != passwordHash)
            {
                // Increase failed login attempt and on 3rd invalid attempt block user
                await this._mediator.Send<Guid>(new InvalidLoginAttemptedCommand
                {
                    Id = user.Id
                });
                throw new DomainException("Invalid Credentials");
            }
            if (user.Status != Domain.Enums.UserStatus.Active)
            {
                throw new DomainException("User Is Not Active");
            }
            IEnumerable<int>? permissions = null;

            // If user is not super user, then get permissions based on role
            if (!user.IsSuperUser)
            {
                var permissionsSpec = RolePermissionSpecifications.GetByRoleIdsSpec(user.UserRoles.Select(x => x.RoleId));
                permissions = (await _unitOfWork.Repository<RolePermission>().ListAsync(permissionsSpec, false))
                    .Select(x => x.PermissionId);
            }
            // if user is super user, then get all available permissions
            else
            {
                permissions = (await _unitOfWork.Repository<Permission>().ListAllAsync(false)).Select(x => x.Id);
            }

            // Set last login date for user
            await this._mediator.Send<Guid>(new LoginSuccessCommand
            {
                Id = user.Id
            });

            var secretKey = _config.GetValue<string>("JWTSecretKey");
            var token = JWTHelper.GenerateJwtToken(user.EmployeeId.ToString(), secretKey);
            var refreshToken = JWTHelper.GenerateRefreshToken();
            await this._mediator.Send<Guid>(new SetRefreshTokenCommand
            {
                Id = user.Id,
                Token = refreshToken
            });

            return new LoginRes()
            {
                Id = user.EmployeeId,
                FirstName = user.Employee.FirstName,
                LastName = user.Employee.LastName,
                MiddleName = user.Employee.MiddleName,
                Token = token,
                Permissions = permissions,
                RefreshToken = refreshToken
            };
        }

        public async Task<GetAllUsersRes> Handle(GetAllUsersReq request, CancellationToken cancellationToken)
        {
            BaseSpecification<User> spec;
            if (!string.IsNullOrWhiteSpace(request.SearchKeyword))
            {
                spec = UserSpecifications.SearchUsersSpec(request.SearchKeyword);
            }
            else
            {
                spec = UserSpecifications.GetAllUsersSpec();
            }
            var count = await _unitOfWork.Repository<User>().CountAsync(spec);
            if (request.PageSize > 0)
            {
                spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            }
            var data = await _unitOfWork.Repository<User>().ListAsync(spec, false);
            var result = data.Select(x => new UserViewModel
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                LastLogInOn = x.LastLogInOn,
                InValidLogInAttemps = x.InValidLogInAttemps,
                Status = x.Status,
                StatusText = x.Status.GetDescription(),
                EmployeeCode = x.Employee.EmployeeCode,
                FirstName = x.Employee.FirstName,
                LastName = x.Employee.LastName,
                MiddleName = x.Employee.MiddleName,
                EmailId = x.Employee.OfficeEmailId,
                MobileNo = x.Employee.OfficeContactNo
            }).ToList();

            return new GetAllUsersRes
            {
                Result = result,
                Count = count
            };
        }

        public async Task<UserViewModel> Handle(GetUserByIdReq request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            spec.AddInclude("UserRoles");
            spec.AddInclude("UserRoles.Role");

            var user = await _unitOfWork.Repository<User>().SingleAsync(spec, false);

            var userRoles = user.UserRoles.Select(x => new UserRoleViewModel
            {
                Id = x.Id,
                RoleId = x.RoleId,
                RoleName = x.Role.Name
            });

            return new UserViewModel
            {
                Id = user.Id,
                EmployeeId = user.EmployeeId,
                LastLogInOn = user.LastLogInOn,
                InValidLogInAttemps = user.InValidLogInAttemps,
                Status = user.Status,
                StatusText = user.Status.GetDescription(),
                EmployeeCode = user.Employee.EmployeeCode,
                FirstName = user.Employee.FirstName,
                LastName = user.Employee.LastName,
                MiddleName = user.Employee.MiddleName,
                EmailId = user.Employee.OfficeEmailId,
                MobileNo = user.Employee.OfficeContactNo,
                UserRoles = userRoles
            };
        }

        public async Task<ValidateRefreshTokenRes> Handle(ValidateRefreshTokenReq request, CancellationToken cancellationToken)
        {
            var secretKey = _config.GetValue<string>("JWTSecretKey");
            var claims = JWTHelper.ValidateTokenWithoutLifeTIme(request.Token, secretKey);
            if (!claims.Any())
            {
                throw new DomainException("Token is not valid");
            }

            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var userId = Guid.Parse(userClaims.Claims.First(x => x.Type == "nameid").Value);

            var userSpec = UserSpecifications.GetUserByEmployeeIdSpec(userId);
            var user = await _unitOfWork.Repository<User>().SingleAsync(userSpec, false);
            user.ValidateRefreshToken(request.RefreshToken);

            var token = JWTHelper.GenerateJwtToken(user.EmployeeId.ToString(), secretKey);

            return new ValidateRefreshTokenRes
            {
                Token = token,
                RefreshToken = request.RefreshToken
            };
        }
    }
}