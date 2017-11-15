using System;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertWorkSetting : FullAuditedEntity<long>
    {
        public int Week { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
