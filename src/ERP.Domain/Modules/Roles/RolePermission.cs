using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;

namespace ERP.Domain.Modules.Roles
{
    public class RolePermission : BaseAuditableEntity
    {
        public RolePermission()
        { }

        private RolePermission(Guid id, Guid roleId, int permissionId, Guid createdBy)
        {
            Id = id;
            RoleId = roleId;
            PermissionId = permissionId;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        #region Behaviours
        public static RolePermission CreateRolePermission(Guid roleId, int permissionId, Guid createdBy)
        {
            Guard.Against.Null(roleId, "Role Id");
            Guard.Against.Null(permissionId, "Permission Id");
            Guard.Against.NumberLessThan(permissionId, "Permission Id", 0);
            Guard.Against.Null(createdBy, "Created By");

            return new RolePermission(Guid.NewGuid(), roleId, permissionId, createdBy);
        }

        public void RemoveRolePermission(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            IsDeleted = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }
        #endregion

        #region States
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; protected set; }
        public Permission Permission { get; protected set; }
        #endregion
    }
}