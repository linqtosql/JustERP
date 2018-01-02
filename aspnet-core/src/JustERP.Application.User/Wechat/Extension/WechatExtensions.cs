using JustERP.Application.User.Wechat.Dto;
using Senparc.Weixin.MP.TenPayLibV3;

namespace JustERP.Application.User.Wechat.Extension
{
    public static class WechatExtensions
    {
        public static bool TradeSuccess(this OrderQueryResult order)
        {
            return order.IsResultCodeSuccess() && order.IsReturnCodeSuccess() &&
                   order.trade_state == PaymentStates.SUCCESS;
        }
    }
}
