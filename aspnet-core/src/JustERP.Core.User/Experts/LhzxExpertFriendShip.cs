using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertFriendShip : FullAuditedEntity<long>
    {
        public long ExpertId { get; set; }
        public long ExpertFriendId { get; set; }
    }
}
