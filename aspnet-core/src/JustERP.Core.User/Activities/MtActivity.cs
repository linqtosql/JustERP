using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Activities
{
    public class MtActivity : AuditedEntity<long>, IDeletionAudited, ILocalizationAudited
    {
        [MaxLength(32), Required]
        public string Name { get; set; }

        [MaxLength(64), Required]
        public string Icon { get; set; }

        public long? PeopleId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public float Turn { get; set; }

        public virtual MtPeople People { get; set; }

        public virtual IEnumerable<MtPeopleActivity> PeopleActivities { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
        [MaxLength(16)]
        public string Language { get; set; }
    }
}
