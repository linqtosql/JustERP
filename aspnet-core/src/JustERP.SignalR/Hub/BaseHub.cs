using Abp.Dependency;

namespace JustERP.SignalR.Hub
{
    public abstract class BaseHub : Microsoft.AspNetCore.SignalR.Hub, ITransientDependency
    {
        protected BaseHub()
        {

        }
    }
}
