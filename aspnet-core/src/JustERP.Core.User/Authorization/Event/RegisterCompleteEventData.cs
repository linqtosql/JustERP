using Abp.Events.Bus;
using JustERP.Core.User.Pepoles;

namespace JustERP.Core.User.Authorization.Event
{
    public class RegisterCompleteEventData : EventData
    {
        public MtPeople Register { get; set; }
    }
}
