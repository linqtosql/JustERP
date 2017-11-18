using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpert : FullAuditedEntity<long>, IFullAudited<Authorization.Users.User>
    {
        public long ExpertClassId { get; set; }
        [MaxLength(16)]
        public string Phone { get; set; }
        [MaxLength(32)]
        public string Password { get; set; }
        [MaxLength(16)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Avatar { get; set; }
        [MaxLength(128)]
        public string BackgroundImage { get; set; }
        [MaxLength(32)]
        public string JobOrg { get; set; }
        [MaxLength(32)]
        public string Job { get; set; }
        public int? WorkYears { get; set; }
        [MaxLength(64)]
        public string Tags { get; set; }
        [MaxLength(512)]
        public string Profile { get; set; }
        public double? Score { get; set; }
        public double? AvgTime { get; set; }
        public int? ServicesCount { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsActive { get; set; }
        public virtual LhzxExpertClass ExpertClass { get; set; }
        public virtual IEnumerable<LhzxExpertWorkSetting> ExpertWorkSettings { get; set; }
        public virtual IEnumerable<LhzxExpertComment> ExpertComments { get; set; }
        [CanBeNull]
        public virtual Authorization.Users.User CreatorUser { get; set; }
        [CanBeNull]
        public virtual Authorization.Users.User LastModifierUser { get; set; }
        [CanBeNull]
        public virtual Authorization.Users.User DeleterUser { get; set; }
    }
}
