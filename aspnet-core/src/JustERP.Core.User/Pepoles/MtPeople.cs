using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Activities;

namespace JustERP.Core.User.Pepoles
{
    public class MtPeople : AuditedEntity<long>
    {
        public string NickName { get; set; }
        public string AvatarImg { get; set; }
        public string Openid { get; set; }
        public virtual MtPeopleWechatInfo PeopleWechatInfo { get; set; }
        public virtual IEnumerable<MtActivity> Activities { get; set; }
        public virtual IEnumerable<MtPeopleActivity> PeopleActivities { get; set; }
    }
}
