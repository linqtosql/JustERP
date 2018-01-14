using System;
using Abp.Events.Bus.Handlers;

namespace JustERP.Application.User.Orders.Events
{
    public class OrderPayedHandler : IEventHandler<OrderPayedEventData>
    {
        public void HandleEvent(OrderPayedEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
