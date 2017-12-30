using System.Collections.Generic;
using System.Threading.Tasks;
using JustERP.Application.User.Experts.Dto;
using JustERP.Application.User.Orders.Dto;
using JustERP.Application.User.Wechat.Dto;

namespace JustERP.Application.User.Orders
{
    public interface IExpertOrderAppService
    {
        /// <summary>
        /// 创建订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<long> CreateOrder(CreateExpertOrderInput input);

        /// <summary>
        /// 创建支付订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<UnifiedOrderDto> CreateOrderPayment(CreateOrderPaymentInput input);
        Task<List<ExpertOrderDto>> GetLoggedIndExpertOrders(GetExpertOrdersInput input);
        Task<ExpertOrderDetailsDto> GetExpertOrderDetail(long orderId);
        Task<ExpertCommentDto> GetExpertOrderComment(long orderId);

        Task<ExpertOrderDto> CancelOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> RefuseOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> AcceptOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> PayOrder(string orderNo);
        Task<ExpertOrderDto> CompleteOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> CommentOrder(CommentOrderInput input);
    }
}