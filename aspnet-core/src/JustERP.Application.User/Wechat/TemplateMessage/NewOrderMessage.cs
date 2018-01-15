using Senparc.Weixin.Entities.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;

namespace JustERP.Application.User.Wechat.TemplateMessage
{
    /// <summary>
    /// 新订单的提醒
    /// </summary>
    public class NewOrderMessage : TemplateMessageBase
    {
        public TemplateDataItem first { get; set; } = new TemplateDataItem("客户下单提醒");
        public TemplateDataItem keyword1 { get; set; }
        public TemplateDataItem keyword2 { get; set; }
        public TemplateDataItem keyword3 { get; set; } = new TemplateDataItem("联合咨询");
        public TemplateDataItem remark { get; set; } = new TemplateDataItem("您有一个新的订单5分钟内需要处理。");
        public NewOrderMessage(string templateId, string url, string templateName) : base(templateId, url, templateName)
        {
        }

        public NewOrderMessage() : this("tzU-T9QvXy3B62LHS8pvZS1Gpxofi-jG0_VIO-FCCV0", "", "新订单提醒")
        {

        }
    }
}
