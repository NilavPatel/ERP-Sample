using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Exceptions;

namespace ERP.Domain.Modules.Leaves
{
    public class LeaveType : BaseAuditableEntity
    {
        public LeaveType()
        { }

        private LeaveType(Guid id, string name, string description, bool isActive, bool countInPayroll, Guid createdBy)
        {
            Id = Id;
            Name = name;
            Description = description;
            IsActive = isActive;
            CountInPayroll = countInPayroll;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public static LeaveType Create(string name, string description,
            bool isActive, bool countInPayroll,
            Guid createdBy, Func<string, Task<bool>> isNameAleradyExist)
        {
            Guard.Against.NullOrWhiteSpace(name, "Name");
            Guard.Against.MaximumLength(name, "Name", 20);
            Guard.Against.MaximumLength(description, "Description", 200);
            Guard.Against.Null(createdBy, "Created By");

            var isExist = isNameAleradyExist(name).ConfigureAwait(false).GetAwaiter().GetResult();
            if (isExist)
            {
                throw new DomainException("Name Already Exist");
            }

            return new LeaveType(Guid.NewGuid(), name, description, isActive, countInPayroll, createdBy);
        }

        public void UpdateLeaveType(string name, string description,
            bool isActive, bool countInPayroll,
            Guid modifiedBy, Func<Guid, string, Task<bool>> isNameAleradyExist)
        {
            Guard.Against.NullOrWhiteSpace(name, "Name");
            Guard.Against.MaximumLength(name, "Name", 20);
            Guard.Against.MaximumLength(description, "Description", 200);
            Guard.Against.Null(modifiedBy, "Modified By");

            var isExist = isNameAleradyExist(Id, name).ConfigureAwait(false).GetAwaiter().GetResult();
            if (isExist)
            {
                throw new DomainException("Name Already Exist");
            }

            Name = name;
            Description = description;
            IsActive = isActive;
            CountInPayroll = countInPayroll;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void DeleteLeaveType(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            IsDeleted = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        #region States
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool CountInPayroll { get; set; }
        #endregion
    }
}