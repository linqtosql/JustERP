using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using JustERP.Application.User.Charts.Dto;
using JustERP.Charts;

namespace JustERP.SignalR.Hub
{
    public class ExpertChatHub : BaseHub
    {
        private IExpertChatService _chatService;
        public ExpertChatHub(IExpertChatService chatService)
        {
            _chatService = chatService;
        }
        public override async Task OnConnectedAsync()
        {
            await Clients.All.InvokeAsync("Send", $"{Context.ConnectionId} joined");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.InvokeAsync("Send", $"{Context.ConnectionId} left");
        }

        public async Task SendToGroup(string groupName, CreateExpertChatInput chat)
        {
            var chatDto = await _chatService.CreateExpertChat(chat);
            await Clients.Group(groupName).InvokeAsync("SendToGroup", chatDto);
        }

        public async Task JoinGroup(string groupName)
        {
            await Groups.AddAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).InvokeAsync("Send", $"{Context.ConnectionId} joined {groupName}");
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).InvokeAsync("Send", $"{Context.ConnectionId} left {groupName}");
        }

        public Task Echo(string message)
        {
            return Clients.Client(Context.ConnectionId).InvokeAsync("Send", $"{Context.ConnectionId}: {message}");
        }
    }
}
