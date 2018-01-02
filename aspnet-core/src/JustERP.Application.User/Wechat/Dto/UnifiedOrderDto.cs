using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat.Dto
{
    public class UnifiedOrderDto
    {
        public UnifiedOrderDto(string appId, string timeStamp, string nonceStr, string prePayId, string signType = "MD5")
        {
            AppId = appId;
            TimeStamp = timeStamp;
            NonceStr = nonceStr;
            Package = $"prepay_id={prePayId}";
            SignType = signType;
        }
        public string AppId { get; set; }
        public string TimeStamp { get; set; }
        public string NonceStr { get; set; }
        public string Package { get; }
        public string SignType { get; }
        public string PaySign { get; private set; }

        public void Sign(string secret)
        {
            PaySign = TenPayV3.GetJsPaySign(AppId, TimeStamp, NonceStr, Package, secret, SignType);
        }
    }
}
