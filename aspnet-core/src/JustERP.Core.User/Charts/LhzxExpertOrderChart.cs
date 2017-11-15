using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Charts
{
    public class LhzxExpertOrderChart : FullAuditedEntity<long>
    {
        public long ExpertOrderId { get; set; }
        public long ExpertId { get; set; }
        public long ExperReceiverId { get; set; }
        public string Content { get; set; }
    }
}

