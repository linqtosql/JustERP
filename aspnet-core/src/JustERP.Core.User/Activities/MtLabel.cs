using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Activities
{
    public class MtLabel : Entity<long>, IDeletionAudited
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }

        public long LabelCategoryId { get; set; }
        public long? PeopleId { get; set; }

        public virtual MtLabelCategory LabelCategory { get; set; }
        public virtual MtPeople People { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletionTime { get; set; }
        public long? DeleterUserId { get; set; }
    }
}
