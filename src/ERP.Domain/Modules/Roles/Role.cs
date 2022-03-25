using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Exceptions;

namespace ERP.Domain.Modules.Roles
{
    public class Role : AggregateRoot
    {
        public Role()
        { }

        private Role(string name, string? description, Guid createdBy)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        #region Behaviours
        public static Role CreateRole(string name, string? description, Guid createdBy,
            Func<string, Task<bool>> isNameAlreadyExist)
        {
            Guard.Against.NullOrWhiteSpace(name, "Name");
            Guard.Against.MaximumLength(name, "Name", 20);
            Guard.Against.MaximumLength(description ?? string.Empty, "Description", 200);
            Guard.Against.Null(createdBy, "Created By");
            var isNotValid = isNameAlreadyExist(name).ConfigureAwait(false).GetAwaiter().GetResult();
            if (isNotValid)
            {
                throw new DomainException("Role Name Already Exist");
            }

            return new Role(name, description, createdBy);
        }

        public void UpdateRole(string name, string? description, Guid modifiedBy,
            Func<Guid, string, Task<bool>> isNameAlreadyExist)
        {
            Guard.Against.NullOrWhiteSpace(name, "Name");
            Guard.Against.MaximumLength(name, "Name", 20);
            Guard.Against.MaximumLength(description ?? string.Empty, "Description", 200);
            Guard.Against.Null(modifiedBy, "Modified By");
            var isNotValid = isNameAlreadyExist(Id, name).ConfigureAwait(false).GetAwaiter().GetResult();
            if (isNotValid)
            {
                throw new DomainException("Role Name Already Exist");
            }

            Name = name;
            Description = description;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void DeleteRole(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            IsDeleted = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }
        #endregion

        #region States
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}