using System.Collections.Generic;
using System.Threading.Tasks;
using JustERP.Application.User.Orders.Dto;

namespace JustERP.Application.User.Orders
{
    public interface IExpertOrderAppService
    {
        Task<long> CreateOrder(CreateExpertOrderInput input);
        Task<List<ExpertOrderDto>> GetLoggedIndExpertOrders(GetExpertOrdersInput input);
        Task<ExpertOrderDetailsDto> GetExpertOrderDetail(long orderId);
    }
}