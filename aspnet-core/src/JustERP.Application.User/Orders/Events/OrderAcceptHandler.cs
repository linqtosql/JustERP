using System;
using Abp.Events.Bus.Handlers;

namespace JustERP.Application.User.Orders.Events
{
    public class OrderAcceptHandler : IEventHandler<OrderAcceptEventData>
    {
        public void HandleEvent(OrderAcceptEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
