using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertAccount : AuditedEntity<long>, ISoftDelete
    {
        [Required]
        [MaxLength(16)]
        public string UserName { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual LhzxExpert Expert { get; set; }
    }
}
