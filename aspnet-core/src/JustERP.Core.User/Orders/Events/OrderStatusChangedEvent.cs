using Abp.Events.Bus;

namespace JustERP.Core.User.Orders.Events
{
    public class OrderStatusChangedEvent : EventData

    {
        public ExpertOrderStatus FromStatus { get; set; }
        public ExpertOrderStatus ToStatus { get; set; }
        public LhzxExpertOrder ChangedOrder { get; set; }
    }
}
