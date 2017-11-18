using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Orders;

namespace JustERP.Core.User.Charts
{
    public class LhzxExpertOrderChart : FullAuditedEntity<long>
    {
        public long ExpertOrderId { get; set; }
        public long ExpertId { get; set; }
        public long ExperReceiverId { get; set; }
        [MaxLength(512)]
        public string Content { get; set; }
        public virtual LhzxExpert SenderExpert { get; set; }
        public virtual LhzxExpert ReceiverExpert { get; set; }
        public virtual LhzxExpertOrder ExpertOrder { get; set; }
    }
}

