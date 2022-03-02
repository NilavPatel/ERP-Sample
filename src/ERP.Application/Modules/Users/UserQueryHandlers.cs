using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ERP.Application.Core.Repositories;
using ERP.Application.Core.Services;
using ERP.Core.Helpers;
using ERP.Domain.Core.Specifications;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Roles;
using ERP.Domain.Modules.Users;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ERP.Application.Modules.Users
{
    public class LoginQueryHandler : IRequestHandler<LoginReq, LoginRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        public LoginQueryHandler(IUnitOfWork unitOfWork, IEncryptionService encryptionService,
         IConfiguration configuration, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
            _config = configuration;
            _mediator = mediator;
        }

        public async Task<LoginRes> Handle(LoginReq request, CancellationToken cancellationToken)
        {
            var userSpec = UserSpecifications.GetUserByEmployeeCodeSpec(request.EmployeeCode);
            userSpec.AddInclude(x => x.Employee);
            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(userSpec, false);
            if (user == null || user.Employee == null)
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

            var permissionsSpec = RolePermissionSpecifications.GetByRoleIdSpec(user.RoleId);
            var permissions = (await _unitOfWork.Repository<RolePermission>().ListAsync(permissionsSpec, false))
                .Select(x => x.PermissionId);

            // Set last login date for user
            await this._mediator.Send<Guid>(new LoginSuccessCommand
            {
                Id = user.Id
            });

            var token = GenerateJwtToken(user);

            return new LoginRes()
            {
                Id = user.Id,
                FirstName = user.Employee.FirstName,
                LastName = user.Employee.LastName,
                MiddleName = user.Employee.MiddleName,
                Token = token,
                Permissions = permissions
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("nameid", user.Id.ToString()),
            };

            // generate token that is valid for 2 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _config.GetValue<string>("JWTSecretKey");
            if (string.IsNullOrWhiteSpace(secretKey) || secretKey.Length < 24)
            {
                throw new ArgumentException("JWT secret key not available.");
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class GetAllUsersReqQueryHandler : IRequestHandler<GetAllUsersReq, GetAllUsersRes>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetAllUsersReqQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            spec.AddInclude(x => x.Employee);
            spec.AddInclude(x => x.Role);
            spec.ApplyPaging((request.PageIndex * request.PageSize), request.PageSize);
            var data = await _unitOfWork.Repository<User>().ListAsync(spec, false);
            var result = data.Select(x => new UserViewModel
            {
                Id = x.Id,
                EmployeeId = x.EmployeeId,
                LastLogInOn = x.LastLogInOn,
                InValidLogInAttemps = x.InValidLogInAttemps,
                Status = x.Status,
                StatusText = x.Status.GetDescription(),
                RoleId = x.RoleId,
                RoleName = x.Role.Name,
                EmployeeCode = x.Employee.EmployeeCode,
                FirstName = x.Employee.FirstName,
                LastName = x.Employee.LastName,
                MiddleName = x.Employee.MiddleName,
                EmailId = x.Employee.OfficeEmailId,
                MobileNo = x.Employee.OfficeContactNo,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOn,
                ModifiedOn = x.ModifiedOn,
                ModifiedBy = x.ModifiedBy,
                IsDeleted = x.IsDeleted
            });
            return new GetAllUsersRes
            {
                Result = result,
                Count = count
            };
        }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserById, UserViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserViewModel> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var spec = UserSpecifications.GetUserByIdSpec(request.Id);
            spec.AddInclude(x => x.Employee);
            spec.AddInclude(x => x.Role);
            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(spec, false);
            if (user == null)
            {
                throw new DomainException("User Is Not Found");
            }
            return new UserViewModel
            {
                Id = user.Id,
                EmployeeId = user.EmployeeId,
                LastLogInOn = user.LastLogInOn,
                InValidLogInAttemps = user.InValidLogInAttemps,
                Status = user.Status,
                StatusText = user.Status.GetDescription(),
                RoleId = user.RoleId,
                RoleName = user.Role.Name,
                EmployeeCode = user.Employee.EmployeeCode,
                FirstName = user.Employee.FirstName,
                LastName = user.Employee.LastName,
                MiddleName = user.Employee.MiddleName,
                EmailId = user.Employee.OfficeEmailId,
                MobileNo = user.Employee.OfficeContactNo,
                CreatedBy = user.CreatedBy,
                CreatedOn = user.CreatedOn,
                ModifiedOn = user.ModifiedOn,
                ModifiedBy = user.ModifiedBy,
                IsDeleted = user.IsDeleted
            };
        }
    }
}