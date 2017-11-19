using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertClass : FullAuditedEntity<long>, IFullAudited<Authorization.Users.User>
    {
        [MaxLength(16)]
        public string Name { get; set; }
        public long? ParentId { get; set; }
        [MaxLength(128)]
        public string Background { get; set; }
        public int Turn { get; set; }
        public virtual IEnumerable<LhzxExpertClass> ChildrenExpertClasses { get; set; }
        public virtual IEnumerable<LhzxExpert> Experts { get; set; }
        public virtual IEnumerable<LhzxExpert> FirstClassExperts { get; set; }
        public virtual Authorization.Users.User CreatorUser { get; set; }
        public virtual Authorization.Users.User LastModifierUser { get; set; }
        public virtual Authorization.Users.User DeleterUser { get; set; }
    }
}
