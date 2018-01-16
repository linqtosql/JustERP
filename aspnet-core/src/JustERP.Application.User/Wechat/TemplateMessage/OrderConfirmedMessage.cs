using System;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace JustERP.Application.User.Wechat.TemplateMessage
{
    /// <summary>
    /// 订单已确认提醒
    /// </summary>
    public class OrderConfirmedMessage : TemplateMessageBase
    {
        public TemplateDataItem first { get; set; } = new TemplateDataItem("专家确认提醒");
        public TemplateDataItem keyword1 { get; set; }
        public TemplateDataItem keyword2 { get; set; }
        public TemplateDataItem keyword3 { get; set; }
        public TemplateDataItem remark { get; set; } = new TemplateDataItem("您的订单专家已确认，请前往及时支付。");
        public OrderConfirmedMessage(string templateId, string url, string templateName) : base(templateId, url, templateName)
        {
        }

        public OrderConfirmedMessage(long orderId, string orderNo, decimal amount, DateTime orderTime) : this("Uj5Tb8DR05Fqkkc_dB-2rqgna4_7jmxjeODmWQ30Ji4", null, "订单确认通知")
        {
            keyword1 = new TemplateDataItem(orderNo);
            keyword2 = new TemplateDataItem(amount.ToString("0.00"));
            keyword3 = new TemplateDataItem(orderTime.ToString("yyyy年MM月dd日"));
            Url = $"https://www.advisors-ally.com/#/order/detail/{orderId}";
        }
    }
}