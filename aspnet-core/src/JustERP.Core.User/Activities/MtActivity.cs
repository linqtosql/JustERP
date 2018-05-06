using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Activities
{
    public class MtActivity : AuditedEntity<long>
    {
        [MaxLength(32), NotNull]
        public string Name { get; set; }

        [MaxLength(64), Required]
        public string Icon { get; set; }

        public long? PeopleId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public float Turn { get; set; }

        public virtual MtPeople People { get; set; }
    }
}
