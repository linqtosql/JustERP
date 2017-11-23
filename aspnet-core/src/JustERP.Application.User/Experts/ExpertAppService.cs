using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq;
using JustERP.Application.User.Experts.Dto;
using JustERP.Core.User.Experts;
using Microsoft.EntityFrameworkCore;

namespace JustERP.Application.User.Experts
{
    public class ExpertAppService : ApplicationService, IExpertAppService
    {
        public IRepository<LhzxExpert, long> ExpertRepository { get; set; }
        public IRepository<LhzxExpertClass, long> ExpertClassRepository { get; set; }
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        private ExpertManager _expertManager;

        public ExpertAppService(ExpertManager expertManager)
        {
            _expertManager = expertManager;
        }

        public async Task<List<ExpertClassDto>> GetGroupedByClassExperts()
        {
            var query = ExpertClassRepository.GetAllIncluding(e => e.Experts).Where(e => e.ParentId != null);
            var list = await AsyncQueryableExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<ExpertClassDto>>(list);
        }

        public async Task<ExpertDetailsDto> GetExpertDetail(long accountId)
        {
            var expert = await ExpertRepository.GetAllIncluding(
                e => e.ExpertClass,
                e => e.ExpertFirstClass,
                e => e.ExpertComments,
                e => e.ExpertWorkSettings)
                .SingleOrDefaultAsync(e => e.ExpertAccountId == accountId);

            return ObjectMapper.Map<ExpertDetailsDto>(expert);
        }

        public async Task<ExpertDto> GetExpertLoginInfo()
        {
            var expert = await ExpertRepository.GetAll()
                .SingleOrDefaultAsync(e => e.ExpertAccountId == AbpSession.UserId);
            return ObjectMapper.Map<ExpertDto>(expert);
        }

        [AbpAuthorize]
        public async Task UpdateNonExpert(CreateNonExpertInput input)
        {
            var expert = ObjectMapper.Map<LhzxExpert>(input);
            await ExpertRepository.UpdateAsync(expert);
        }

        [AbpAuthorize]
        public async Task UpdateExpert(CreateExpertInput input)
        {
            var expert = ObjectMapper.Map<LhzxExpert>(input);
            await ExpertRepository.UpdateAsync(expert);
        }
    }
}
