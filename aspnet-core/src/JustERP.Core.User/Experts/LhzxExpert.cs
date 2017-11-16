using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpert : FullAuditedEntity<long>, IFullAudited<Authorization.Users.User>
    {
        public long ExpertClassId { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string BackgroundImage { get; set; }
        public string JobOrg { get; set; }
        public string Job { get; set; }
        public int? WorkYears { get; set; }
        public string Tags { get; set; }
        public string Profile { get; set; }
        public double? Score { get; set; }
        public double? AvgTime { get; set; }
        public int? ServicesCount { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsActive { get; set; }
        public virtual LhzxExpertClass ExpertClass { get; set; }
        public virtual IEnumerable<LhzxExpertComment> ExpertComments { get; set; }
        public virtual Authorization.Users.User CreatorUser { get; set; }
        public virtual Authorization.Users.User LastModifierUser { get; set; }
        public virtual Authorization.Users.User DeleterUser { get; set; }
    }
}
