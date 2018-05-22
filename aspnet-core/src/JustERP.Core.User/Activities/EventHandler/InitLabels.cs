using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using JustERP.Core.User.Authorization.Event;

namespace JustERP.Core.User.Activities.EventHandler
{
    public class InitLabels : IEventHandler<RegisterCompleteEventData>, ITransientDependency
    {
        private ActivityManager _activityManager;
        public InitLabels(ActivityManager activityManager)
        {
            _activityManager = activityManager;
        }
        
        public void HandleEvent(RegisterCompleteEventData eventData)
        {
            _activityManager.InitLabels(eventData.Register);
        }
    }
}
