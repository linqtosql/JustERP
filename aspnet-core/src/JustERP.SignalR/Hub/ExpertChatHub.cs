using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Abp.Auditing;
using Abp.RealTime;
using JustERP.Application.User.Charts.Dto;
using JustERP.Charts;

namespace JustERP.SignalR.Hub
{
    public class ExpertChatHub : BaseHub
    {
        private IExpertChatService _chatService;
        private IOnlineClientManager _onlineClientManager;

        public ExpertChatHub(
            IExpertChatService chatService,
            IOnlineClientManager onlineClientManager,
            IClientInfoProvider clientInfoProvider) : base(onlineClientManager, clientInfoProvider)
        {
            _chatService = chatService;
            _onlineClientManager = onlineClientManager;
        }

        public async Task SendToGroup(string groupName, CreateExpertChatInput chat)
        {
            var chatDto = await _chatService.CreateExpertChat(chat);
            await Clients.Group(groupName).SendAsync("SendToGroup", chatDto);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddAsync(Context.ConnectionId, groupName);
            await Clients.Group(groupName).SendAsync("JoinGroup", AbpSession.UserId);
        }

        public async Task StartChat(string groupName)
        {
            await Clients.Group(groupName).SendAsync("StartChat", AbpSession.UserId);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveAsync(Context.ConnectionId, groupName);
            await Clients.OthersInGroup(groupName).SendAsync("LeaveGroup", AbpSession.UserId);
        }

    }
}
