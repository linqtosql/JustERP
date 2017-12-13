using System.Collections.Generic;
using System.Threading.Tasks;
using JustERP.Application.User.Experts.Dto;
using JustERP.Application.User.Orders.Dto;

namespace JustERP.Application.User.Orders
{
    public interface IExpertOrderAppService
    {
        Task<long> CreateOrder(CreateExpertOrderInput input);
        Task<List<ExpertOrderDto>> GetLoggedIndExpertOrders(GetExpertOrdersInput input);
        Task<ExpertOrderDetailsDto> GetExpertOrderDetail(long orderId);
        Task<ExpertCommentDto> GetExpertOrderComment(long orderId);

        Task<ExpertOrderDto> CancelOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> RefuseOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> AcceptOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> PayOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> CompleteOrder(GetExpertOrderInput input);
        Task<ExpertOrderDto> CommentOrder(CommentOrderInput input);

    }
}