using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertFriendShip : CreationAuditedEntity<long>
    {
        public long ExpertId { get; set; }
        public long ExpertFriendId { get; set; }
        
        public virtual LhzxExpert ExpertFriend { get; set; }
    }
}
