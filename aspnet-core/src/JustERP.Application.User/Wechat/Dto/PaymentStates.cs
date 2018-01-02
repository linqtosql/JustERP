namespace JustERP.Application.User.Wechat.Dto
{
    public class PaymentStates
    {
        /// <summary>
        /// 支付成功
        /// </summary>
        public const string SUCCESS = "SUCCESS";

        /// <summary>
        /// 转入退款
        /// </summary>
        public const string REFUND = "REFUND";

        /// <summary>
        /// 未支付
        /// </summary>
        public const string NOTPAY = "NOTPAY";

        /// <summary>
        /// 已关闭
        /// </summary>
        public const string CLOSED = "CLOSED";

        /// <summary>
        /// 已撤销（刷卡支付）
        /// </summary>
        public const string REVOKED = "REVOKED";

        /// <summary>
        /// 用户支付中
        /// </summary>
        public const string USERPAYING = "USERPAYING";

        /// <summary>
        /// 支付失败
        /// </summary>
        public const string PAYERROR = "PAYERROR";
    }
}