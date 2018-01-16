using System;
using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace JustERP.Application.User.Wechat.TemplateMessage
{
    /// <summary>
    /// 订单支付成功通知
    /// </summary>
    public class PayedSuccessMessage : TemplateMessageBase
    {
        public TemplateDataItem first { get; set; } = new TemplateDataItem("订单支付提醒");
        public TemplateDataItem keyword1 { get; set; }
        public TemplateDataItem keyword2 { get; set; }
        public TemplateDataItem keyword3 { get; set; } = new TemplateDataItem("微信");
        public TemplateDataItem keyword4 { get; set; }
        public TemplateDataItem remark { get; set; } = new TemplateDataItem("客户已付款，前往查看。");

        private PayedSuccessMessage(string templateId, string url, string templateName) : base(templateId, url, templateName)
        {
        }

        public PayedSuccessMessage(long orderId, string orderNo, decimal amount, DateTime orderTime) : this("jeuoTwoKZpe3FDxPjc1cHVbvdGbGoJCncmE-1YKYbKk", null, "付款成功通知")
        {
            keyword1 = new TemplateDataItem(orderNo);
            keyword2 = new TemplateDataItem(amount.ToString("0.00"));
            keyword4 = new TemplateDataItem(orderTime.ToString("yyyy年MM月dd日"));
            Url = $"https://www.advisors-ally.com/#/order/detail/{orderId}";
        }
    }
}