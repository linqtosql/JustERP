using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using JustERP.Application.User.Orders.Dto;

namespace JustERP.Application.User.Orders
{
    public interface IExpertOrderAppService
    {
        Task<long> CreateOrder(CreateExpertOrderInput input);
        Task<List<ExpertOrderDto>> GetLoggedIndExpertOrders(PagedResultRequestDto input);
    }
}