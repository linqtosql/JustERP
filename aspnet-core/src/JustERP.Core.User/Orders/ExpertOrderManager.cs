using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using JustERP.Core.User.Experts;

namespace JustERP.Core.User.Orders
{
    public class ExpertOrderManager : DomainService
    {
        private static Random _random;
        private IRepository<LhzxExpertOrder, long> _orderRepository;
        private IRepository<LhzxExpertOrderLog, long> _orderLogRepository;
        public ExpertOrderManager(IRepository<LhzxExpertOrder, long> orderRepository,
            IRepository<LhzxExpertOrderLog, long> orderLogRepository)
        {
            _orderLogRepository = orderLogRepository;
            _orderRepository = orderRepository;
        }

        static ExpertOrderManager()
        {
            _random = new Random();
        }

        public async Task<LhzxExpertOrder> CreateOrder(LhzxExpert expert, LhzxExpert serviceExpert, LhzxExpertOrder order)
        {
            if (!serviceExpert.IsExpert)
            {
                throw new UserFriendlyException("只能向专家发起咨询");
            }
            order.ServerExpertId = serviceExpert.Id;
            order.ExpertId = expert.Id;
            order.Status = (int)ExpertOrderStatus.Waiting;
            order.CreationTime = DateTime.Now;
            order.OrderNo = $"{DateTime.Now:yyyyMMdd}{_random.Next(100000, 999999)}";
            order.IsDeleted = false;
            order.Price = serviceExpert.Price ?? 0;
            order.Amount = order.Price * order.Quantity;
            order = await _orderRepository.InsertAsync(order);
            await UnitOfWorkManager.Current.SaveChangesAsync();

            await _orderLogRepository.InsertAsync(new LhzxExpertOrderLog
            {
                ExpertOrderId = order.Id,
                CreationTime = DateTime.Now,
                Title = "提交了订单"
            });

            return order;
        }
    }

    public enum ExpertOrderStatus
    {
        /// <summary>
        /// 待确认
        /// </summary>
        Waiting = 1,
        /// <summary>
        /// 待支付
        /// </summary>
        Paying = 2,
        /// <summary>
        /// 待咨询
        /// </summary>
        Charting = 3,
        /// <summary>
        /// 咨询完成
        /// </summary>
        Complete = 4,
        /// <summary>
        /// 已评价
        /// </summary>
        Commented = 5,
        /// <summary>
        /// 客户已取消
        /// </summary>
        Canceled = 6,
        /// <summary>
        /// 已拒绝
        /// </summary>
        Refused = 7
    }
}
