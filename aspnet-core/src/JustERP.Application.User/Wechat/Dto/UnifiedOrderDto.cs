using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat.Dto
{
    public class UnifiedOrderDto
    {
        public string AppId { get; set; }
        public string TimeStamp { get; set; }
        public string NonceStr { get; set; }
        public string Package { get; set; }
        public string SignType { get; set; } = "MD5";
        public string PaySign { get; private set; }

        public void Sign(string secret)
        {
            PaySign = TenPayV3.GetJsPaySign(AppId, TimeStamp, NonceStr, Package, secret, SignType);
        }
    }
}
