using Abp.Timing;
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
        public TemplateDataItem keyword3 { get; set; } = new TemplateDataItem(Clock.Now.ToString("yyyy年MM月dd日"));
        public TemplateDataItem remark { get; set; } = new TemplateDataItem("您的订单专家已确认，请前往及时支付。");
        public OrderConfirmedMessage(string templateId, string url, string templateName) : base(templateId, url, templateName)
        {
        }

        public OrderConfirmedMessage() : this("Uj5Tb8DR05Fqkkc_dB-2rqgna4_7jmxjeODmWQ30Ji4", "", "订单确认提醒")
        {

        }
    }
}