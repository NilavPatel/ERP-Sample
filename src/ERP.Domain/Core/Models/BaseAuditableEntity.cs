namespace ERP.Domain.Core.Models
{
    public class BaseAuditableEntity: BaseEntity
    {
        public Guid CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}