using Abp.Timing;
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
        public TemplateDataItem keyword4 { get; set; } = new TemplateDataItem(Clock.Now.ToString("yyyy年MM月dd日"));
        public TemplateDataItem remark { get; set; } = new TemplateDataItem("客户已付款，前往查看。");
        public PayedSuccessMessage(string templateId, string url, string templateName) : base(templateId, url, templateName)
        {
        }

        public PayedSuccessMessage() : this("jeuoTwoKZpe3FDxPjc1cHVbvdGbGoJCncmE-1YKYbKk", "", "付款成功提醒")
        {
            
        }
    }
}