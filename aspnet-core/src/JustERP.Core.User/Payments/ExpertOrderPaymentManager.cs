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
            await _orderPaymentRepository.DeleteAsync(p => p.ExpertOrderId == order.Id);

            orderPayment.Create(order);

            await _orderPaymentRepository.InsertAsync(orderPayment);

            return orderPayment;
        }
    }
}
