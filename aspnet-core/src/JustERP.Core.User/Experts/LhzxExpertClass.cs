using System.Collections.Generic;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertClass : FullAuditedEntity<long>, IFullAudited<Authorization.Users.User>
    {
        public string Name { get; set; }
        public long? ParentId { get; set; }
        public string Background { get; set; }
        public int Turn { get; set; }
        public virtual IEnumerable<LhzxExpert> Experts { get; set; }
        public virtual Authorization.Users.User CreatorUser { get; set; }
        public virtual Authorization.Users.User LastModifierUser { get; set; }
        public virtual Authorization.Users.User DeleterUser { get; set; }
    }
}
