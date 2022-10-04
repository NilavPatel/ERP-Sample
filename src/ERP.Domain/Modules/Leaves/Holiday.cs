using ERP.Domain.Core.GuardClauses;
using ERP.Domain.Core.Models;
using ERP.Domain.Exceptions;

namespace ERP.Domain.Modules.Leaves
{
    public class Holiday : BaseAuditableEntity
    {
        public Holiday() { }

        private Holiday(Guid id, string name, DateTimeOffset holidayOn, Guid createdBy)
        {
            Id = id;
            Name = name;
            HolidayOn = holidayOn;
            CreatedBy = createdBy;
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public static Holiday Create(string name, DateTimeOffset holidayOn, Guid createdBy, Func<DateTimeOffset, Task<bool>> IsHolidayExist)
        {
            Guard.Against.NullOrWhiteSpace(name, "Name");
            Guard.Against.MaximumLength(name, "Name", 20);
            Guard.Against.Null(holidayOn, "Holiday On");
            Guard.Against.Null(createdBy, "Created By");

            var isExist = IsHolidayExist(holidayOn).ConfigureAwait(false).GetAwaiter().GetResult();
            if (isExist)
            {
                throw new DomainException("Holiday On This Date Already Exist");
            }

            return new Holiday(Guid.NewGuid(), name, holidayOn, createdBy);
        }

        public void UpdateHoliday(string name, DateTimeOffset holidayOn, Guid modifiedBy, Func<Guid, DateTimeOffset, Task<bool>> IsHolidayExist)
        {
            Guard.Against.NullOrWhiteSpace(name, "Name");
            Guard.Against.MaximumLength(name, "Name", 20);
            Guard.Against.Null(holidayOn, "Holiday On");
            Guard.Against.Null(modifiedBy, "Modified By");

            var isExist = IsHolidayExist(Id, holidayOn).ConfigureAwait(false).GetAwaiter().GetResult();
            if (isExist)
            {
                throw new DomainException("Holiday On This Date Already Exist");
            }

            Name = name;
            HolidayOn = holidayOn;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public void DeleteHoliday(Guid modifiedBy)
        {
            Guard.Against.Null(modifiedBy, "Modified By");

            IsDeleted = true;
            ModifiedBy = modifiedBy;
            ModifiedOn = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset HolidayOn { get; set; }
    }
}