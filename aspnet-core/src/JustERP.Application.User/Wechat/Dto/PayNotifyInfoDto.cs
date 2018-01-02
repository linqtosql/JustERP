using Abp.AutoMapper;
using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat.Dto
{
    [AutoMapFrom(typeof(ResponseHandler))]
    public class PayNotifyInfoDto
    {
        /// <summary>
        /// 公众账号ID
        /// </summary>
        public string appid { get; set; }
        /// <summary>
        /// 支付测试
        /// </summary>
        public string attach { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        public string bank_type { get; set; }
        /// <summary>
        /// 货币种类
        /// </summary>
        public string fee_type { get; set; }
        /// <summary>
        /// 是否关注公众账号
        /// </summary>
        public string is_subscribe { get; set; }
        /// <summary>
        /// 商户号
        /// </summary>
        public string mch_id { get; set; }
        /// <summary>
        /// 随机字符串	
        /// </summary>
        public string nonce_str { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string out_trade_no { get; set; }
        /// <summary>
        /// 业务结果(SUCCESS/FAIL)
        /// </summary>
        public string result_code { get; set; }
        /// <summary>
        /// 返回状态码(SUCCESS/FAIL)
        /// </summary>
        public string return_code { get; set; }
        /// <summary>
        /// 返回信息
        /// </summary>
        public string return_msg { get; set; }
        /// <summary>
        /// sign
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string sub_mch_id { get; set; }
        /// <summary>
        /// 支付完成时间(格式为yyyyMMddHHmmss)
        /// </summary>
        public string time_end { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public int total_fee { get; set; }
        /// <summary>
        /// 交易类型(JSAPI、NATIVE、APP)
        /// </summary>
        public string trade_type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string transaction_id { get; set; }
    }
}
