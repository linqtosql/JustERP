using System;
using Abp.Domain.Entities;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertWorkSetting : Entity<long>
    {
        public long ExpertId { get; set; }
        public int Week { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual LhzxExpert Expert { get; set; }
    }
}
