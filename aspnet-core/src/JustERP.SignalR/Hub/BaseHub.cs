using Abp.AspNetCore.SignalR.Hubs;
using Abp.Auditing;
using Abp.RealTime;

namespace JustERP.SignalR.Hub
{
    public abstract class BaseHub : AbpCommonHub
    {
        protected BaseHub(IOnlineClientManager onlineClientManager, IClientInfoProvider clientInfoProvider) : base(onlineClientManager, clientInfoProvider)
        {
        }
    }
}
