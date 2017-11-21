using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertAccount : AuditedEntity<long>, ISoftDelete
    {
        public string UserName { get; set; }
        public DateTime LastLoginTime { get; set; }
        public bool IsDeleted { get; set; }
        public virtual LhzxExpert Expert { get; set; }
    }
}
