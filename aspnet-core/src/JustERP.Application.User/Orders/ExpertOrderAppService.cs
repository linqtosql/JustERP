using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.UI;
using JustERP.Application.User.Experts.Dto;
using JustERP.Application.User.Orders.Dto;
using JustERP.Application.User.Wechat;
using JustERP.Application.User.Wechat.Dto;
using JustERP.Application.User.Wechat.Extension;
using JustERP.Core.User.Experts;
using JustERP.Core.User.Orders;
using JustERP.Core.User.Payments;
using JustERP.Core.User.Wechat;
using Microsoft.EntityFrameworkCore;

namespace JustERP.Application.User.Orders
{
    [AbpAuthorize]
    public class ExpertOrderAppService : ApplicationService, IExpertOrderAppService
    {
        private IRepository<LhzxExpertOrder, long> _orderRepository;
        private IRepository<LhzxExpert, long> _expertRepository;
        private IRepository<LhzxExpertComment, long> _commentRepository;
        private IRepository<LhzxExpertWechatInfo, long> _expertWechatRepository;
        private IRepository<LhzxExpertOrderPayment, long> _orderPaymentRepository;

        public ExpertOrderManager OrderManager { get; set; }
        public ExpertOrderPaymentManager OrderPaymentManager { get; set; }
        public ExpertWechatAppService WechatAppService { get; set; }
        public ExpertOrderAppService(IRepository<LhzxExpertOrder, long> ordeRepository,
            IRepository<LhzxExpert, long> expertRepository,
            IRepository<LhzxExpertComment, long> commentRepository,
            IRepository<LhzxExpertWechatInfo, long> expertWechatRepository,
            IRepository<LhzxExpertOrderPayment, long> orderPaymentRepository)
        {
            _orderRepository = ordeRepository;
            _expertRepository = expertRepository;
            _commentRepository = commentRepository;
            _expertWechatRepository = expertWechatRepository;
            _orderPaymentRepository = orderPaymentRepository;
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

        public async Task<UnifiedOrderDto> CreateOrderPayment(CreateOrderPaymentInput input)
        {
            var order = await _orderRepository.GetAsync(input.ExpertOrderId);
            var expert = await _expertRepository.GetAsync(AbpSession.UserId.Value);
            var wechatInfo = await _expertWechatRepository.SingleAsync(w => w.Openid == expert.OpenId);

            await OrderPaymentManager.CreateOrder(new LhzxExpertOrderPayment
            {
                Account = wechatInfo.Nickname,
                PaymentChannel = (int)PaymentChannels.Wechat
            }, order);

            var unifiedOrderDto = await WechatAppService.Unifiedorder(
                new CreateUnifiedOrderInput(order.OrderNo, "联合咨询服务费", order.Amount, expert.OpenId));
            return unifiedOrderDto;
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

            query = query.OrderByDescending(o => o.Id);

            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);

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

        public async Task<ExpertCommentDto> GetExpertOrderComment(long orderId)
        {
            var orderComments = await _commentRepository.GetAllIncluding(
                e => e.CommenterExpert,
                e => e.ExpertCommentReplies)
                .Where(e => e.ExpertOrderId == orderId && e.ParentId == null)
                .SingleOrDefaultAsync();

            return ObjectMapper.Map<ExpertCommentDto>(orderComments);
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

        /// <summary>
        /// 订单付款
        /// </summary>
        /// <param name="resultInput"></param>
        /// <returns></returns>
        [RemoteService(false)]
        public async Task<ExpertOrderDto> PayOrder(CreatePaymentResultInput resultInput)
        {
            var order = await _orderRepository.SingleAsync(o => o.OrderNo == resultInput.OrderNo);
            CheckIfMineOrder(order);
            CheckIsPayingOrder(order);
            //订单为已支付状态
            await OrderManager.PayOrder(order);

            var orderPayment = await _orderPaymentRepository.SingleAsync(p => p.ExpertOrderId == order.Id);

            ObjectMapper.Map(resultInput, orderPayment);
            orderPayment.SetData("PaymentContent", resultInput.PaymentContent);
            //支付订单为已完成状态
            await OrderPaymentManager.PaymentComplete(orderPayment);

            return ObjectMapper.Map<ExpertOrderDto>(order);
        }

        public async Task<ExpertOrderDto> GetPaymentStatus(long orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);
            var wechatOrder = await WechatAppService.QueryOrder(new QueryOrderInput(order.OrderNo));
            if (wechatOrder.TradeSuccess())
            {
                return await PayOrder(new CreatePaymentResultInput
                {
                    OrderNo = wechatOrder.out_trade_no,
                    PaymentContent = wechatOrder,
                    PaymentNo = wechatOrder.transaction_id,
                    PaymentTime = DateTime.Now
                });
            }
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
            var commenter = _expertRepository.GetAsync(input.CommenterExpertId);
            var expert = _expertRepository.GetAsync(order.ServerExpertId);
            var result = await Task.WhenAll(commenter, expert);

            var comment = ObjectMapper.Map<LhzxExpertComment>(input);

            await OrderManager.CommentOrder(order, result[0], result[1], comment);
            return ObjectMapper.Map<ExpertOrderDto>(order);
        }

        private void CheckIsPayingOrder(LhzxExpertOrder order)
        {
            if (order.Status != (int)ExpertOrderStatus.Paying) throw new UserFriendlyException("订单已取消或已支付");
        }
    }
}
