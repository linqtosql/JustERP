using System;
using Abp.AutoMapper;
using JustERP.Core.User.Payments;

namespace JustERP.Application.User.Orders.Dto
{
    [AutoMapTo(typeof(LhzxExpertOrderPayment))]
    public class CreatePaymentResultInput
    {
        public string OrderNo { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PaymentNo { get; set; }

        public DateTime PaymentTime { get; set; }
        /// <summary>
        /// 支付完成后支付接口返回的信息对象
        /// </summary>
        public object PaymentContent { get; set; }
    }
}
