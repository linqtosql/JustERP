using System;

namespace JustERP.Application.User.Wechat.Dto
{
    public class SendOrderMessageInput
    {
        public long OrderId { get; set; }
        public string OpenId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderTime { get; set; }
        public string ExpertName { get; set; }
        public string ExpertPhone { get; set; }
        public string ServerExpertName { get; set; }
        public decimal OrderAmount { get; set; }
    }
}
