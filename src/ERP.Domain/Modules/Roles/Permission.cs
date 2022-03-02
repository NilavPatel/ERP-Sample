using System.ComponentModel.DataAnnotations.Schema;
using ERP.Domain.Core.Models;

namespace ERP.Domain.Modules.Roles
{
    public class Permission : BaseEntity
    {
        #region States
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        #endregion
    }
}