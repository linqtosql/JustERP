using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat.Dto
{
    public class CreateUnifiedOrderInput
    {
        public CreateUnifiedOrderInput(string tradeNo, string productName, decimal amount, string openId)
        {
            TradeNo = tradeNo;
            ProductName = productName;
            Amount = (int)(amount * 100);
            OpenId = openId;
        }
        public string TradeNo { get; set; }
        public string TimeStamp { get; set; } = TenPayV3Util.GetTimestamp();
        public string NonceStr { get; set; } = TenPayV3Util.GetNoncestr();
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string OpenId { get; set; }
    }
}
