using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Abp.Runtime.Session;
using JustERP.Application.User.Charts.Dto;
using JustERP.Charts;

namespace JustERP.SignalR.Hub
{
    public class ExpertChatHub : BaseHub
    {
        private IExpertChatService _chatService;

        public IAbpSession AbpSession { get; set; }
        public ExpertChatHub(IExpertChatService chatService)
        {
            _chatService = chatService;
        }

        public async Task SendToGroup(string groupName, CreateExpertChatInput chat)
        {
            var chatDto = await _chatService.CreateExpertChat(chat);
            await Clients.Group(groupName).InvokeAsync("SendToGroup", chatDto);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveAsync(Context.ConnectionId, groupName);
        }
    }
}
