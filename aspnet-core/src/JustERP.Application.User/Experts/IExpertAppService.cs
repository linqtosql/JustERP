using System.Collections.Generic;
using System.Threading.Tasks;
using JustERP.Application.User.Experts.Dto;

namespace JustERP.Application.User.Experts
{
    public interface IExpertAppService
    {
        /// <summary>
        /// 获取按分类分组后的专家列表
        /// </summary>
        /// <returns></returns>
        Task<List<ExpertClassDto>> GetGroupedByClassExperts();

        Task<ExpertDetailsDto> GetExpertDetail(GetExpertDetailInput input);
        Task<List<ExpertCommentDto>> GetExpertComments(long expertId);

        Task<CreateNonExpertInput> GetNonExpert();

        Task<CreateExpertInput> GetExpert();

        Task<List<ExpertClassDto>> GetAllExpertClasses();

        Task<List<ExpertDto>> GetExperts(SearchExpertInput input);

        Task<LoggedInExpertOutput> GetExpertLoginInfo();

        Task CreateNonExpert(CreateNonExpertInput input);

        Task CreateExpert(CreateExpertInput input);

        Task<ExpertPriceDto> GetExpertPrice(long expertId);

        Task<ExpertDto> ChangeOnlineStatusTo(ChangeOnlineStatusInput input);
        Task<ExpertFriendDto> CreateExpertFriend(CreateExpertFriendDto input);
        Task<List<ExpertFriendDto>> GetExpertFriends();
    }
}
