using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.UI;
using JustERP.Core.User.Orders;

namespace JustERP.Core.User.Payments
{
    public class ExpertOrderPaymentManager : DomainService
    {
        private IRepository<LhzxExpertOrderPayment, long> _orderPaymentRepository;

        public ExpertOrderPaymentManager(IRepository<LhzxExpertOrderPayment, long> orderPaymentRepository)
        {
            _orderPaymentRepository = orderPaymentRepository;
        }

        public async Task<LhzxExpertOrderPayment> CreateOrder(LhzxExpertOrderPayment orderPayment, LhzxExpertOrder order)
        {
            var existsPayment = await _orderPaymentRepository.FirstOrDefaultAsync(p => p.ExpertOrderId == order.Id);
            if (existsPayment != null && existsPayment.Status == (int)PaymentStatus.PayComplete)
            {
                throw new UserFriendlyException("订单已支付，请刷新页面查看");
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
