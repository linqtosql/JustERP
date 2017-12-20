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
        private IRepository<LhzxExpert, long> _expertRepository;
        private IRepository<LhzxExpertOrder, long> _orderRepository;
        private IRepository<LhzxExpertOrderLog, long> _orderLogRepository;
        private IRepository<LhzxExpertComment, long> _commentRepository;
        public ExpertOrderManager(IRepository<LhzxExpertOrder, long> orderRepository,
            IRepository<LhzxExpertOrderLog, long> orderLogRepository,
            IRepository<LhzxExpertComment, long> commentRepository,
            IRepository<LhzxExpert, long> expertRepository)
        {
            _orderLogRepository = orderLogRepository;
            _orderRepository = orderRepository;
            _commentRepository = commentRepository;
            _expertRepository = expertRepository;
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

            await CreateOrderLog(order);

            return order;
        }

        public async Task CancelOrder(LhzxExpertOrder order)
        {
            order.Status = (int)ExpertOrderStatus.Canceled;
            await CreateOrderLog(order);
        }

        public async Task RefuseOrder(LhzxExpertOrder order)
        {
            order.Status = (int)ExpertOrderStatus.Refused;
            await CreateOrderLog(order);
        }

        public async Task AcceptOrder(LhzxExpertOrder order)
        {
            order.Status = (int)ExpertOrderStatus.Paying;
            await CreateOrderLog(order);
        }

        public async Task PayOrder(LhzxExpertOrder order)
        {
            order.Status = (int)ExpertOrderStatus.Charting;
            await CreateOrderLog(order);
        }

        public async Task CompleteOrder(LhzxExpertOrder order)
        {
            order.Status = (int)ExpertOrderStatus.Complete;
            await CreateOrderLog(order);
        }

        public async Task CommentOrder(LhzxExpertOrder order, LhzxExpert commenter, LhzxExpert expert, LhzxExpertComment comment)
        {
            expert.ServicesCount = expert.ServicesCount ?? 0;
            expert.ServicesCount += 1;
            expert.Score = comment.Score;
            order.Status = (int)ExpertOrderStatus.Commented;
            comment.ExpertId = expert.Id;
            comment.CommenterExpertId = commenter.Id;
            await Task.WhenAll(
                            _expertRepository.UpdateAsync(expert),
                            _orderRepository.UpdateAsync(order),
                            _commentRepository.InsertAsync(comment),
                            CreateOrderLog(order));
        }

        private async Task CreateOrderLog(LhzxExpertOrder order)
        {
            string title = null;
            switch ((ExpertOrderStatus)order.Status)
            {
                case ExpertOrderStatus.Waiting:
                    title = "提交了订单";
                    break;
                case ExpertOrderStatus.Paying:
                    title = "专家已确认";
                    break;
                case ExpertOrderStatus.Charting:
                    title = "客户已支付";
                    break;
                case ExpertOrderStatus.Complete:
                    title = "已完成咨询";
                    break;
                case ExpertOrderStatus.Commented:
                    title = "已完成评价";
                    break;
                case ExpertOrderStatus.Canceled:
                    title = "客户已取消";
                    break;
                case ExpertOrderStatus.Refused:
                    title = "专家已拒绝";
                    break;
            }
            await _orderLogRepository.InsertAsync(new LhzxExpertOrderLog
            {
                ExpertOrderId = order.Id,
                CreationTime = DateTime.Now,
                Title = title
            });
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
        /// 咨询中
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
        Canceled = -1,
        /// <summary>
        /// 已拒绝
        /// </summary>
        Refused = -2
    }
}
