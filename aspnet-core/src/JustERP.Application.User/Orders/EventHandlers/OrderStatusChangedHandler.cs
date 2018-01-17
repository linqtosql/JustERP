using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus.Handlers;
using JustERP.Application.User.Wechat;
using JustERP.Application.User.Wechat.Dto;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Orders;
using JustERP.Core.User.Orders.Events;

namespace JustERP.Application.User.Orders.EventHandlers
{
    [UnitOfWork(IsDisabled = true)]
    public class OrderStatusChangedHandler : IEventHandler<OrderStatusChangedEvent>, ITransientDependency
    {
        private IRepository<LhzxExpert, long> _expertRepository;
        private IExpertWechatAppService _wechatAppService { get; set; }

        public OrderStatusChangedHandler(
            IRepository<LhzxExpert, long> expertRepository,
            IExpertWechatAppService wechatAppService)
        {
            _expertRepository = expertRepository;
            _wechatAppService = wechatAppService;
        }

        /// <summary>
        /// 订单状态改变事件处理
        /// </summary>
        /// <param name="eventData"></param>
        public async void HandleEvent(OrderStatusChangedEvent eventData)
        {
            var expert = await _expertRepository.GetAsync(eventData.ChangedOrder.ExpertId);
            var serverExpert = await _expertRepository.GetAsync(eventData.ChangedOrder.ServerExpertId);
            var order = eventData.ChangedOrder;

            var messageInput = new SendOrderMessageInput
            {
                OrderId = order.Id,
                OrderNo = order.OrderNo,
                OrderAmount = order.Amount,
                OrderTime = order.CreationTime,
                ServerExpertName = serverExpert.Name,
                ExpertName = expert.Name,
                ExpertPhone = expert.Phone
            };
            switch (eventData.ToStatus)
            {
                case ExpertOrderStatus.Waiting:
                    messageInput.OpenId = serverExpert.OpenId;
                    await _wechatAppService.SendNewOrderMessage(messageInput);
                    break;
                case ExpertOrderStatus.Paying:
                    messageInput.OpenId = expert.OpenId;
                    await _wechatAppService.SendOrderConfirmMessage(messageInput);
                    break;
                case ExpertOrderStatus.Charting:
                    messageInput.OpenId = serverExpert.OpenId;
                    await _wechatAppService.SendPayedSuccessMessage(messageInput);
                    break;
            }
        }
    }
}
