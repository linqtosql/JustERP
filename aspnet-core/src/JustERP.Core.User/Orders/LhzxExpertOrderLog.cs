using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Orders
{
    public class LhzxExpertOrderLog : CreationAuditedEntity<long>
    {
        public long ExpertOrderId { get; set; }
        public string Title { get; set; }
    }
}
