using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertComment : FullAuditedEntity<long>
    {
        public long? ParentId { get; set; }
        public long ExpertId { get; set; }
        public long BeCommentExpertId { get; set; }
        public long ExpertOrderId { get; set; }
        public double? Score { get; set; }
        public string Content { get; set; }
    }
}

