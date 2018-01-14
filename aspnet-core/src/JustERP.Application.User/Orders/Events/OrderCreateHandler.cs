using System;
using Abp.Events.Bus.Handlers;

namespace JustERP.Application.User.Orders.Events
{
    public class OrderCreateHandler : IEventHandler<OrderCreateEventData>
    {
        public void HandleEvent(OrderCreateEventData eventData)
        {
            throw new NotImplementedException();
        }
    }
}
