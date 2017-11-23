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

        Task<ExpertDetailsDto> GetExpertDetail(long accountId);

        Task<ExpertDto> GetExpertLoginInfo();

        Task UpdateNonExpert(CreateNonExpertInput input);

        Task UpdateExpert(CreateExpertInput input);
    }
}
