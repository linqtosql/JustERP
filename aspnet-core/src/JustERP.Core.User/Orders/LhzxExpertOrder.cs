using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using JetBrains.Annotations;
using JustERP.Core.User.Charts;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Payments;

namespace JustERP.Core.User.Orders
{
    public class LhzxExpertOrder : FullAuditedEntity<long>, IFullAudited<Authorization.Users.User>
    {
        [MaxLength(16)]
        public string OrderNo { get; set; }
        public long ExpertId { get; set; }
        public long ServerExpertId { get; set; }
        public string QuestionRemark { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Amount { get; set; }
        public int TotalDuration { get; set; }
        public int Status { get; set; }
        [MaxLength(512)]
        public string Remark { get; set; }
        public DateTime? ConfirmDatetime { get; set; }
        /// <summary>
        /// 下单的专家
        /// </summary>
        public virtual LhzxExpert Expert { get; set; }
        /// <summary>
        /// 接单的专家
        /// </summary>
        public virtual LhzxExpert ServerExpert { get; set; }
        public virtual IEnumerable<LhzxExpertOrderChart> ExpertOrderCharts { get; set; }
        public virtual IEnumerable<LhzxExpertComment> ExpertComments { get; set; }
        public virtual IEnumerable<LhzxExpertOrderLog> ExpertOrderLogs { get; set; }
        public virtual LhzxExpertOrderPayment ExpertOrderPayment { get; set; }
        public virtual LhzxExpertOrderRefund ExpertOrderRefund { get; set; }
        [CanBeNull]
        public virtual Authorization.Users.User CreatorUser { get; set; }
        [CanBeNull]
        public virtual Authorization.Users.User LastModifierUser { get; set; }
        [CanBeNull]
        public virtual Authorization.Users.User DeleterUser { get; set; }
    }
}

