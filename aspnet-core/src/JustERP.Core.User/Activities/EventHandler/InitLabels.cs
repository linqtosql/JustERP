using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using JustERP.Core.User.Authorization.Event;

namespace JustERP.Core.User.Activities.EventHandler
{
    public class InitLabels : IEventHandler<RegisterCompleteEventData>, ITransientDependency
    {
        public ActivityManager ActivityManager { get; set; }
        public void HandleEvent(RegisterCompleteEventData eventData)
        {
            ActivityManager.InitLabels(eventData.Register);
        }
    }
}
