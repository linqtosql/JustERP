namespace JustERP.Application.User.Wechat.Dto
{
    public class PayNotifyInfoDto
    {
        public string return_code { get; set; }
        public string return_msg { get; set; }
        public string out_trade_no { get; set; }
        public int total_fee { get; set; }
    }
}
