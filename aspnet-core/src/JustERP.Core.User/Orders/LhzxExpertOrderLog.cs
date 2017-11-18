using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Orders
{
    public class LhzxExpertOrderLog : CreationAuditedEntity<long>
    {
        public long ExpertOrderId { get; set; }
        [MaxLength(64)]
        public string Title { get; set; }
        public virtual LhzxExpertOrder ExpertOrder { get; set; }
    }
}
