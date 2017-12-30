using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using JustERP.Core.User.Orders;

namespace JustERP.Core.User.Payments
{
    public class LhzxExpertOrderPayment : CreationAuditedEntity<long>, IExtendableObject
    {
        public long ExpertId { get; set; }
        /// <summary>
        /// 支付订单号
        /// </summary>
        [MaxLength(32)]
        public string PaymentNo { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public short Status { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public short PaymentChannel { get; set; }
        public long ExpertOrderId { get; set; }
        public string Account { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? PaymentTime { get; set; }
        public string ExtensionData { get; set; }

        public virtual LhzxExpertOrder ExpertOrder { get; set; }

        public void Create(LhzxExpertOrder order)
        {
            Amount = order.Amount;
            ExpertId = order.ExpertId;
            ExpertOrderId = order.Id;
            Status = (int)PaymentStatus.WaitPay;
        }
    }

    public enum PaymentChannels : short
    {
        /// <summary>
        /// 微信支付
        /// </summary>
        Wechat = 1
    }

    public enum PaymentStatus : short
    {
        /// <summary>
        /// 待支付
        /// </summary>
        WaitPay = 1,
        /// <summary>
        /// 支付完成
        /// </summary>
        PayComplete = 2,
        /// <summary>
        /// 支付出错
        /// </summary>
        PayError = 3,
        /// <summary>
        /// 支付取消
        /// </summary>
        PayCanceled = 4
    }
}

