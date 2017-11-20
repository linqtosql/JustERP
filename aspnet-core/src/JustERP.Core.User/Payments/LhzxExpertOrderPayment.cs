using System;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Orders;

namespace JustERP.Core.User.Payments
{
    public class LhzxExpertOrderPayment : CreationAuditedEntity<long>, IExtendableObject
    {
        public long? ExpertId { get; set; }
        public long? ExpertOrderId { get; set; }
        public string Account { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentTime { get; set; }
        public string ExtensionData { get; set; }

        public virtual LhzxExpertOrder ExpertOrder { get; set; }
    }
}

