using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Domain.Repositories;
using JustERP.Application.User.Charts.Dto;
using JustERP.Charts;
using JustERP.Core.User.Charts;
using JustERP.Core.User.Orders;
using Microsoft.EntityFrameworkCore;

namespace JustERP.Application.User.Charts
{
    public class ExpertChatService : ApplicationService, IExpertChatService
    {
        private IRepository<LhzxExpertOrder, long> _orderRepository;
        private IRepository<LhzxExpertOrderChart, long> _chatRepository;
        public ExpertChatService(IRepository<LhzxExpertOrder, long> orderRepository,
            IRepository<LhzxExpertOrderChart, long> chatRepository)
        {
            _orderRepository = orderRepository;
            _chatRepository = chatRepository;
        }

        public async Task<ExpertOrderChatDto> GetExpertOrderChats(long orderId)
        {
            var order = await _orderRepository.GetAsync(orderId);

            var chats = await _chatRepository.GetAllIncluding(
                c => c.SenderExpert,
                c => c.ReceiverExpert,
                c => c.ExpertOrder)
                .Where(c => c.ExpertOrderId == orderId).OrderBy(c => c.Id).ToListAsync();

            var orderDto = ObjectMapper.Map<ExpertOrderChatDto>(order);

            orderDto.ExpertOrderCharts = ObjectMapper.Map<List<ExpertChatDto>>(chats);

            return orderDto;
        }

        public async Task<ExpertChatDto> CreateExpertChat(CreateExpertChatInput input)
        {
            var chat = ObjectMapper.Map<LhzxExpertOrderChart>(input);
            chat.CreationTime = DateTime.Now;
            await _chatRepository.InsertAsync(chat);

            return ObjectMapper.Map<ExpertChatDto>(chat);
        }

        public async Task EndChat()
        {
            throw new System.NotImplementedException();
        }
    }
}
