using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using JustERP.Core.User.Orders;

namespace JustERP.Core.User.Payments
{
    public class ExpertOrderPaymentManager : DomainService
    {
        private IRepository<LhzxExpertOrderPayment, long> _orderPaymentRepository;
        private IRepository<LhzxExpertOrder, long> _orderRepository;
        public ExpertOrderPaymentManager(IRepository<LhzxExpertOrderPayment, long> orderPaymentRepository,
            IRepository<LhzxExpertOrder, long> orderRepository)
        {
            _orderPaymentRepository = orderPaymentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<LhzxExpertOrderPayment> CreateOrder(LhzxExpertOrderPayment orderPayment, LhzxExpertOrder order)
        {
            var existsPayment = await _orderPaymentRepository.FirstOrDefaultAsync(p => p.ExpertOrderId == order.Id);
            if (existsPayment != null && existsPayment.Status == (int)PaymentStatus.PayComplete)
            {
                throw new ApplicationException("已支付的订单无法再重新发起支付");
            }
            await _orderPaymentRepository.DeleteAsync(p => p.ExpertOrderId == order.Id);

            orderPayment.Create(order);

            await _orderPaymentRepository.InsertAsync(orderPayment);

            return orderPayment;
        }

        /// <summary>
        /// 支付成功
        /// </summary>
        /// <param name="orderPayment"></param>
        /// <returns></returns>
        public async Task PaymentComplete(LhzxExpertOrderPayment orderPayment)
        {
            orderPayment.Status = (short)PaymentStatus.PayComplete;
            await _orderPaymentRepository.UpdateAsync(orderPayment);
        }
    }
}
