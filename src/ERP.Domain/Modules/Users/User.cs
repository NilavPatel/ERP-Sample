using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Enums;
using ERP.Domain.Exceptions;
using ERP.Domain.Modules.Employees;

namespace ERP.Domain.Modules.Users
{
    public class User : AggregateRoot
    {
        public User()
        { }

        #region Behaviours
        private User(
            Guid id,
            Guid employeeId,
            string passwordHash,
            string saltKey,
            Guid createdBy)
        {
            Id = id;
            EmployeeId = employeeId;
            PasswordHash = passwordHash;
            SaltKey = saltKey;
            Status = UserStatus.Active;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public static User Create(
            Guid employeeId,
            string passwordHash,
            string saltKey,
            Guid createdBy,
            Func<Guid, Task<bool>> isUserExist)
        {
            Guard.Against.Null(createdBy, "Created By");
            Guard.Against.Null(employeeId, "Employee Id");
            Guard.Against.NullOrWhiteSpace(saltKey, "Salt Key");
            Guard.Against.NullOrWhiteSpace(passwordHash, "Password Hash");
            var isNotValid = isUserExist(employeeId).ConfigureAwait(false).GetAwaiter().GetResult();
            if (isNotValid)
            {
                throw new DomainException("User Already Exist.");
            }

            return new User(
                Guid.NewGuid(),
                employeeId,
                passwordHash,
                saltKey,
                createdBy);
        }

        public void ResetUserPassword(
            string passwordHash,
            string saltKey,
            Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");
            Guard.Against.NullOrWhiteSpace(saltKey, "Salt Key");
            Guard.Against.NullOrWhiteSpace(passwordHash, "Password Hash");

            PasswordHash = passwordHash;
            SaltKey = saltKey;
            if (Status == UserStatus.BlockedDueToInvalidLoginAttempts)
            {
                Status = UserStatus.Active;
                InValidLogInAttemps = 0;
            }
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void ActivateUser(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            Status = UserStatus.Active;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void BlockUser(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            Status = UserStatus.Blocked;
            RefreshToken = null;
            RefreshTokenExpiryTime = null;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void InvalidLoginAttempt()
        {
            InValidLogInAttemps++;
            if (InValidLogInAttemps >= 3)
            {
                Status = Domain.Enums.UserStatus.BlockedDueToInvalidLoginAttempts;
            }
        }

        public void LoginSuccessfully()
        {
            InValidLogInAttemps = 0;
            LastLogInOn = DateTimeOffset.UtcNow;
        }

        public void SetRefreshToken(string refreshToken)
        {
            Guard.Against.NullOrWhiteSpace(refreshToken, "RefreshToken");
            Guard.Against.MaximumLength(refreshToken, "RefreshToken", 200);

            RefreshToken = refreshToken;
            RefreshTokenExpiryTime = DateTimeOffset.Now.AddDays(5);
        }

        public void RevokeRefreshToken(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            RefreshToken = null;
            RefreshTokenExpiryTime = null;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            Guard.Against.NullOrWhiteSpace(refreshToken, "RefreshToken");
            Guard.Against.MaximumLength(refreshToken, "RefreshToken", 200);
            if (refreshToken != RefreshToken)
            {
                throw new DomainException("Invalid Refresh Token");
            }

            if (RefreshTokenExpiryTime <= DateTimeOffset.Now)
            {
                throw new DomainException("Refresh Token Has Expired");
            }

            return true;
        }

        #endregion

        #region Stats
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string PasswordHash { get; set; }
        public string SaltKey { get; set; }
        public DateTimeOffset? LastLogInOn { get; set; }
        public byte InValidLogInAttemps { get; set; }
        public UserStatus Status { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiryTime { get; set; }
        public bool IsSuperUser { get; set; }

        public Employee Employee { get; protected set; }
        public ICollection<UserRole> UserRoles { get; protected set; }
        #endregion
    }
}