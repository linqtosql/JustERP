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

        /// <summary>
        /// 消息类型（文本，图片，语音）
        /// </summary>
        public short ChatType { get; set; } = (short)ChatTypes.Text;
        [MaxLength(512)]
        public string Content { get; set; }
        public virtual LhzxExpert SenderExpert { get; set; }
        public virtual LhzxExpert ReceiverExpert { get; set; }
        public virtual LhzxExpertOrder ExpertOrder { get; set; }
    }

    public enum ChatTypes : short
    {
        /// <summary>
        /// 文本消息
        /// </summary>
        Text = 1,
        /// <summary>
        /// 图片消息
        /// </summary>
        Image = 2,
        /// <summary>
        /// 语音
        /// </summary>
        Voice = 3
    }
}

