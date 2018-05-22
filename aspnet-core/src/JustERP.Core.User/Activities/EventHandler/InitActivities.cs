using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using JustERP.Core.User.Authorization.Event;

namespace JustERP.Core.User.Activities.EventHandler
{
    public class InitActivities : IEventHandler<RegisterCompleteEventData>, ITransientDependency
    {
        private ActivityManager _activityManager;

        public InitActivities(ActivityManager activityManager)
        {
            _activityManager = activityManager;
        }
        public void HandleEvent(RegisterCompleteEventData eventData)
        {
            _activityManager.InitActivities(eventData.Register);
        }
    }
}
