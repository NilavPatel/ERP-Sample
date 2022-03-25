using ERP.Core.Helpers;
using ERP.Domain.Core.Services;
using ERP.Domain.Enums;
using ERP.Domain.Modules.Designations;
using ERP.Domain.Modules.Employees;
using ERP.Domain.Modules.Roles;
using ERP.Domain.Modules.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.Infrastructure.Data
{
    public class DataInitializer
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DbContextOptions<ERPDbContext> _dbContextOptions;
        private readonly IEncryptionService _encryptionService;
        public DataInitializer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _dbContextOptions = _serviceProvider.GetRequiredService<DbContextOptions<ERPDbContext>>();
            _encryptionService = _serviceProvider.GetRequiredService<IEncryptionService>();
        }

        public void Initialize()
        {
            using (var dbContext = new ERPDbContext(_dbContextOptions))
            {
                if (dbContext.Database.CanConnect())
                {
                    dbContext.Database.Migrate();
                    return;
                }

                dbContext.Database.Migrate();

                #region Add Permissions
                IList<Permission> permissions = new List<Permission>();
                var permissionEnumList = Enum.GetValues(typeof(PermissionEnum)).Cast<PermissionEnum>();
                foreach (var permission in permissionEnumList)
                {
                    var permissionDesc = permission.GetDescription().Split(':');
                    permissions.Add(new Permission()
                    {
                        Id = (int)permission,
                        Name = permission.ToString(),
                        Description = permissionDesc[0],
                        GroupName = permissionDesc[1],
                    });
                }
                dbContext.Permissions.AddRange(permissions);
                dbContext.SaveChanges();

                var roleId = Guid.NewGuid();
                dbContext.Roles.Add(new Role
                {
                    Id = roleId,
                    Name = "Super Admin",
                    Description = "This Role has all rights",
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTimeOffset.UtcNow
                });
                dbContext.SaveChanges();

                foreach (var permission in dbContext.Permissions.ToList())
                {
                    dbContext.RolePermissions.Add(new RolePermission
                    {
                        Id = Guid.NewGuid(),
                        RoleId = roleId,
                        PermissionId = permission.Id,
                        CreatedBy = Guid.Empty,
                        CreatedOn = DateTimeOffset.UtcNow
                    });
                }
                dbContext.SaveChanges();
                #endregion

                var designationId = Guid.NewGuid();
                dbContext.Designations.Add(new Designation
                {
                    Id = designationId,
                    Name = "CEO",
                    Description = "Cheif Executive Officer",
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTimeOffset.UtcNow
                });
                dbContext.SaveChanges();

                #region Add Super Admin
                var employeeId = Guid.NewGuid();
                dbContext.Employees.Add(new Employee()
                {
                    Id = employeeId,
                    FirstName = "Nilav",
                    LastName = "Patel",
                    MiddleName = "Pravinbhai",
                    BirthDate = new DateTime(1992, 10, 28).ToUniversalTime(),
                    BloodGroup = "O+",
                    Gender = Gender.Male,
                    ParmenantAddress = "Address line 1, state, city, 1234",
                    CurrentAddress = null,
                    IsCurrentSameAsParmenantAddress = true,
                    MaritalStatus = MaritalStatus.Married,
                    PersonalEmailId = "nilavpatel1992@gmail.com",
                    PersonalMobileNo = "9876543210",
                    OtherContactNo = "9876543210",
                    EmployeeCode = "00001",
                    OfficeEmailId = "nilav.patel@derp.com",
                    OfficeContactNo = "9898981234",
                    JoiningOn = DateTimeOffset.UtcNow.AddYears(-1),
                    RelievingOn = null,
                    DesignationId = designationId,
                    ReportingToId = null,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTimeOffset.UtcNow
                });

                dbContext.EmployeeBankDetails.Add(new EmployeeBankDetail()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    BankName = "Yes Bank",
                    IFSCCode = "YES00001",
                    BranchAddress = "Ahmedabad, India",
                    AccountNumber = "9876543210123456",
                    PANNumber = "XXXXX123456",
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTimeOffset.UtcNow
                });

                var saltKey = _encryptionService.CreateSaltKey(5);
                dbContext.Users.Add(new User()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = employeeId,
                    SaltKey = saltKey,
                    PasswordHash = _encryptionService.CreatePasswordHash("Password", saltKey),
                    Status = Domain.Enums.UserStatus.Active,
                    RoleId = roleId,
                    CreatedBy = Guid.Empty,
                    CreatedOn = DateTimeOffset.UtcNow
                });
                dbContext.SaveChanges();
                #endregion
            }
        }
    }
}
