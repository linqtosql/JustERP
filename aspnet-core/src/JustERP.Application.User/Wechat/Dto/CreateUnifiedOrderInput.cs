namespace JustERP.Application.User.Wechat.Dto
{
    public class CreateUnifiedOrderInput : BaseWechatInput
    {
        public CreateUnifiedOrderInput(string tradeNo, string productName, decimal amount, string openId)
        {
            TradeNo = tradeNo;
            ProductName = productName;
            Amount = (int)(amount * 100);
            OpenId = openId;
        }
        public string TradeNo { get; set; }

        public string ProductName { get; set; }
        public int Amount { get; set; }
        public string OpenId { get; set; }
    }
}
