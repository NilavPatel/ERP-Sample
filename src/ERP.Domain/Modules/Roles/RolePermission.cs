using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;

namespace ERP.Domain.Modules.Roles
{
    public class RolePermission : BaseAuditableEntity
    {
        public RolePermission()
        { }

        private RolePermission(Guid roleId, int permissionId, Guid createdBy)
        {
            RoleId = roleId;
            PermissionId = permissionId;
            CreatedBy = createdBy;
            CreatedOn = DateTime.UtcNow;
        }

        #region Behaviours
        public static RolePermission CreateRolePermission(Guid roleId, int permissionId, Guid createdBy)
        {
            Guard.Against.Null(roleId, "Role Id");
            Guard.Against.Null(permissionId, "Permission Id");
            Guard.Against.NumberLessThan(permissionId, "Permission Id", 0);
            Guard.Against.Null(createdBy, "Created By");

            return new RolePermission(roleId, permissionId, createdBy);
        }

        public void RemoveRolePermission(Guid modifiedBy)
        {
            IsDeleted = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTime.UtcNow;
        }
        #endregion

        #region States
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public int PermissionId { get; set; }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
        #endregion
    }
}