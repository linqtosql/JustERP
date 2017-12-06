using System.Threading.Tasks;
using JustERP.Application.User.Charts.Dto;

namespace JustERP.Charts
{
    public interface IExpertChatService
    {
        Task<ExpertOrderChatDto> GetExpertOrderChats(long orderId);
        Task<ExpertChatDto> CreateExpertChat(CreateExpertChatInput input);
        Task EndChat();
    }
}