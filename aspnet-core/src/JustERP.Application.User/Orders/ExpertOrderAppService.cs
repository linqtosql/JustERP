using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
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
        private IRepository<LhzxExpert, long> _expertRepository;
        public ExpertOrderManager OrderManager { get; set; }
        public ExpertOrderAppService(IRepository<LhzxExpertOrder, long> ordeRepository,
            IRepository<LhzxExpert, long> expertRepository)
        {
            _orderRepository = ordeRepository;
            _expertRepository = expertRepository;
        }

        public async Task<long> CreateOrder(CreateExpertOrderInput input)
        {
            if (!AbpSession.UserId.HasValue) throw new ApplicationException("当前用户必须登录");

            if (AbpSession.UserId.Value == input.ServerExpertId) throw new ApplicationException("您不能自己咨询自己哦");

            var expert = await _expertRepository.GetAsync(AbpSession.UserId.Value);
            var serviceExpert = await _expertRepository.GetAsync(input.ServerExpertId);

            var order = ObjectMapper.Map<LhzxExpertOrder>(input);

            order = await OrderManager.CreateOrder(expert, serviceExpert, order);

            return order.Id;
        }

        public async Task<List<ExpertOrderDto>> GetLoggedIndExpertOrders(GetExpertOrdersInput input)
        {
            if (input.ExpertId != AbpSession.UserId && input.ServerExpertId != AbpSession.UserId)
            {
                throw new ApplicationException("只能获取自己的订单");
            }

            var query = _orderRepository.GetAllIncluding(
                e => e.Expert,
                e => e.ServerExpert);

            if (input.ExpertId > 0) query = query.Where(o => o.ExpertId == input.ExpertId);
            if (input.ServerExpertId > 0) query = query.Where(o => o.ServerExpertId == input.ServerExpertId);

            query = query
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .OrderByDescending(o => o.Id);

            var list = await query.ToListAsync();

            return ObjectMapper.Map<List<ExpertOrderDto>>(list);
        }

        public async Task<ExpertOrderDetailsDto> GetExpertOrderDetail(long orderId)
        {
            var order = await _orderRepository.GetAllIncluding(
                e => e.Expert,
                e => e.ServerExpert,
                e => e.ExpertOrderLogs).SingleOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return new ExpertOrderDetailsDto();

            if (order.ExpertId != AbpSession.UserId && order.ServerExpertId != AbpSession.UserId)
            {
                throw new ApplicationException("只能获取自己的订单");
            }

            return ObjectMapper.Map<ExpertOrderDetailsDto>(order);
        }

        public async Task<ExpertOrderDto> CancelOrder(GetExpertOrderInput input)
        {
            var order = await _orderRepository.GetAsync(input.Id);

            CheckIfCurrentExpertOrder(order);
            CheckIsWaitingOrder(order);

            await OrderManager.CancelOrder(order);

            return ObjectMapper.Map<ExpertOrderDto>(order);
        }

        private void CheckIsWaitingOrder(LhzxExpertOrder order)
        {
            if (order.Status != (int)ExpertOrderStatus.Waiting) throw new UserFriendlyException("订单已取消或已确认");
        }

        private void CheckIfCurrentExpertOrder(LhzxExpertOrder order)
        {
            if (order.ExpertId != AbpSession.UserId) throw new ApplicationException("只能操作自己的订单");
        }

        public async Task<ExpertOrderDto> RefuseOrder(GetExpertOrderInput input)
        {
            var order = await _orderRepository.GetAsync(input.Id);

            CheckIfMineOrder(order);
            CheckIsWaitingOrder(order);

            await OrderManager.RefuseOrder(order);

            return ObjectMapper.Map<ExpertOrderDto>(order);
        }

        private void CheckIfMineOrder(LhzxExpertOrder order)
        {
            if (order.ExpertId != AbpSession.UserId && order.ServerExpertId != AbpSession.UserId)
            {
                throw new ApplicationException("只能操作自己的订单");
            }
        }

        public async Task<ExpertOrderDto> AcceptOrder(GetExpertOrderInput input)
        {
            var order = await _orderRepository.GetAsync(input.Id);

            CheckIfMineOrder(order);
            CheckIsWaitingOrder(order);

            await OrderManager.AcceptOrder(order);

            return ObjectMapper.Map<ExpertOrderDto>(order);
        }

        public async Task<ExpertOrderDto> PayOrder(GetExpertOrderInput input)
        {
            var order = await _orderRepository.GetAsync(input.Id);

            CheckIfMineOrder(order);
            CheckIsPayingOrder(order);

            await OrderManager.PayOrder(order);

            return ObjectMapper.Map<ExpertOrderDto>(order);
        }

        public async Task<ExpertOrderDto> CompleteOrder(GetExpertOrderInput input)
        {
            var order = await _orderRepository.GetAsync(input.Id);
            await OrderManager.CompleteOrder(order);

            return ObjectMapper.Map<ExpertOrderDto>(order);
        }

        public async Task<ExpertOrderDto> CommentOrder(CommentOrderInput input)
        {
            var order = await _orderRepository.GetAsync(input.ExpertOrderId);
            var commenter = await _expertRepository.GetAsync(input.CommenterExpertId);
            var expert = await _expertRepository.GetAsync(order.ServerExpertId);
            var comment = ObjectMapper.Map<LhzxExpertComment>(input);

            await OrderManager.CommentOrder(order, commenter, expert, comment);
            return ObjectMapper.Map<ExpertOrderDto>(order);
        }

        private void CheckIsPayingOrder(LhzxExpertOrder order)
        {
            if (order.Status != (int)ExpertOrderStatus.Paying) throw new UserFriendlyException("订单已取消或已支付");
        }
    }
}
