using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Modules.Roles;

namespace ERP.Domain.Modules.Users
{
    public class UserRole : BaseAuditableEntity
    {
        public UserRole()
        { }

        private UserRole(Guid id, Guid userId, Guid roleId, Guid createdBy)
        {
            Id = id;
            UserId = userId;
            RoleId = roleId;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.Now;
        }

        public static UserRole Create(Guid userId, Guid roleId, Guid createdBy)
        {
            Guard.Against.Null(userId, "User Id");
            Guard.Against.Null(roleId, "Role Id");
            Guard.Against.Null(createdBy, "Created By");

            return new UserRole(Guid.NewGuid(), userId, roleId, createdBy);
        }

        public void Remove(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            IsDeleted = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public User User { get; protected set; }
        public Role Role { get; protected set; }
    }
}