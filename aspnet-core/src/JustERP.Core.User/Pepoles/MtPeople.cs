using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Activities;

namespace JustERP.Core.User.Pepoles
{
    public class MtPeople : AuditedEntity<long>
    {
        private const string DefaultBackground = "#c5c3c6";
        [MaxLength(32)]
        public string NickName { get; set; }
        [MaxLength(256)]
        public string AvatarImg { get; set; }
        [Required,MaxLength(128)]
        public string Openid { get; set; }
        [MaxLength(8)]
        public string BackgroundColor { get; set; } = DefaultBackground;
        public int TimezoneOffset { get; set; }
        [MaxLength(64)]
        public string TimezoneInfo { get; set; }
        public virtual MtPeopleWechatInfo PeopleWechatInfo { get; set; }
        public virtual IEnumerable<MtActivity> Activities { get; set; }
        public virtual IEnumerable<MtPeopleActivity> PeopleActivities { get; set; }
    }
}
