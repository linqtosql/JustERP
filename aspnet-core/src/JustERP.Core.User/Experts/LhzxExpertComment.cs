using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Orders;

namespace JustERP.Core.User.Experts
{
    public class LhzxExpertComment : FullAuditedEntity<long>
    {

        public long? ParentId { get; set; }
        public long ExpertId { get; set; }
        public long CommenterExpertId { get; set; }
        public long ExpertOrderId { get; set; }
        public double Score { get; set; }
        [MaxLength(512)]
        public string Content { get; set; }
        /// <summary>
        /// 评论人
        /// </summary>
        public virtual LhzxExpert CommenterExpert { get; set; }
        /// <summary>
        /// 被评论的专家
        /// </summary>
        public virtual LhzxExpert Expert { get; set; }
        /// <summary>
        /// 评论的回复
        /// </summary>
        public virtual IEnumerable<LhzxExpertComment> ExpertCommentReplies { get; set; }
        /// <summary>
        /// 评论的订单
        /// </summary>
        public virtual LhzxExpertOrder ExpertOrder { get; set; }
    }
}

