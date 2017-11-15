using System;
using Abp.Domain.Entities.Auditing;

namespace JustERP.Core.User.Orders
{
    public class LhzxExpertOrder : FullAuditedEntity<long>, IFullAudited<Authorization.Users.User>
    {
        public string OrderNo { get; set; }
        public long ExpertId { get; set; }
        public long ServerExpertId { get; set; }
        public string QuestionRemark { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public int TotalDuration { get; set; }
        public int Status { get; set; }
        public string Remark { get; set; }
        public DateTime? ConfirmDatetime { get; set; }
        public virtual Authorization.Users.User CreatorUser { get; set; }
        public virtual Authorization.Users.User LastModifierUser { get; set; }
        public virtual Authorization.Users.User DeleterUser { get; set; }
    }
}

