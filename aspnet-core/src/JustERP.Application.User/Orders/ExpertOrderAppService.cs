using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using JustERP.Application.User.Orders.Dto;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Orders;
using Microsoft.EntityFrameworkCore;

namespace JustERP.Application.User.Orders
{
    [AbpAuthorize]
    public class ExpertOrderAppService : ApplicationService, IExpertOrderAppService
    {
        private IRepository<LhzxExpertOrder, long> _orderRepository;
        private IRepository<LhzxExpertOrderLog, long> _orderLogRepository;
        private IRepository<LhzxExpert, long> _expertRepository;
        public ExpertOrderManager OrderManager { get; set; }
        public ExpertOrderAppService(IRepository<LhzxExpertOrder, long> ordeRepository,
            IRepository<LhzxExpertOrderLog, long> orderLogRepository,
            IRepository<LhzxExpert, long> expertRepository)
        {
            _orderLogRepository = orderLogRepository;
            _orderLogRepository = orderLogRepository;
            _expertRepository = expertRepository;
        }

        public async Task<long> CreateOrder(CreateExpertOrderInput input)
        {
            if (!AbpSession.UserId.HasValue) throw new UserFriendlyException("当前用户必须登录");

            var expert = await _expertRepository.GetAsync(AbpSession.UserId.Value);
            var serviceExpert = await _expertRepository.GetAsync(input.ServerExpertId);

            var order = ObjectMapper.Map<LhzxExpertOrder>(input);

            order = await OrderManager.CreateOrder(expert, serviceExpert, order);

            return order.Id;
        }

        public async Task<List<ExpertOrderDto>> GetLoggedIndExpertOrders(PagedResultRequestDto input)
        {
            var query = _orderRepository.GetAllIncluding(
                e => e.Expert,
                e => e.ServerExpert,
                e => e.ExpertOrderLogs)
                .Where(o => o.ExpertId == AbpSession.UserId)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);


            return ObjectMapper.Map<List<ExpertOrderDto>>(await query.ToListAsync());
        }
    }
}
