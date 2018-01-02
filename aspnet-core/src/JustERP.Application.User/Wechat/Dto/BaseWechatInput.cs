using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat.Dto
{
    public class BaseWechatInput
    {
        public string TimeStamp { get; set; } = TenPayV3Util.GetTimestamp();
        public string NonceStr { get; set; } = TenPayV3Util.GetNoncestr();
    }
}