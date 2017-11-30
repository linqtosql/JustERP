﻿using System;
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
        private IRepository<LhzxExpertOrderLog, long> _orderLogRepository;
        private IRepository<LhzxExpert, long> _expertRepository;
        public ExpertOrderManager OrderManager { get; set; }
        public ExpertOrderAppService(IRepository<LhzxExpertOrder, long> ordeRepository,
            IRepository<LhzxExpertOrderLog, long> orderLogRepository,
            IRepository<LhzxExpert, long> expertRepository)
        {
            _orderLogRepository = orderLogRepository;
            _orderRepository = ordeRepository;
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

        public async Task<List<ExpertOrderDto>> GetLoggedIndExpertOrders(GetExpertOrdersInput input)
        {
            if (input.ExpertId != AbpSession.UserId && input.ServerExpertId != AbpSession.UserId)
            {
                throw new ApplicationException("只能获取自己的订单");
            }

            var query = _orderRepository.GetAllIncluding(
                e => e.Expert,
                e => e.ServerExpert)
                .Where(o => o.ExpertId == AbpSession.UserId)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            if (input.ExpertId > 0) query = query.Where(o => o.ExpertId == input.ExpertId);
            if (input.ServerExpertId > 0) query = query.Where(o => o.ServerExpertId == input.ServerExpertId);


            return ObjectMapper.Map<List<ExpertOrderDto>>(await query.ToListAsync());
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
    }
}
