using System.ComponentModel.DataAnnotations;

namespace JustERP.Application.User.Wechat.Dto
{
    public class QueryOrderInput : BaseWechatInput
    {
        public QueryOrderInput(string orderNo)
        {
            TradeNo = orderNo;
        }
        /// <summary>
        /// 订单号
        /// </summary>
        [Required]
        public string TradeNo { get; set; }
    }
}
