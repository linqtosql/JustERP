using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq;
using Abp.UI;
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

        public async Task<ExpertDetailsDto> GetExpertDetail(GetExpertDetailInput input)
        {
            if (!input.Id.HasValue && !input.ExpertAccountId.HasValue)
                throw new UserFriendlyException("Id或者ExpertAccountId必须有一个大于0");
            var expert = await ExpertRepository.GetAllIncluding(
                e => e.ExpertClass,
                e => e.ExpertFirstClass,
                e => e.ExpertComments,
                e => e.ExpertWorkSettings)
                .SingleOrDefaultAsync(e => input.Id > 0 ? e.Id == input.Id : e.ExpertAccountId == input.ExpertAccountId);

            return ObjectMapper.Map<ExpertDetailsDto>(expert);
        }

        public async Task<List<ExpertClassDto>> GetAllExpertClasses()
        {
            var list = await ExpertClassRepository.GetAllIncluding(
                e => e.ChildrenExpertClasses)
                .Where(e => e.ParentId == null).ToListAsync();
            return ObjectMapper.Map<List<ExpertClassDto>>(list);
        }

        public async Task<LoggedInExpertOutput> GetExpertLoginInfo()
        {
            if (!AbpSession.UserId.HasValue) throw new UserFriendlyException("当前用户未登录");

            var expert = await ExpertRepository.GetAll()
                .SingleOrDefaultAsync(e => e.ExpertAccountId == AbpSession.UserId);
            return ObjectMapper.Map<LoggedInExpertOutput>(expert);
        }

        [AbpAuthorize]
        public async Task CreateNonExpert(CreateNonExpertInput input)
        {
            var expert = ObjectMapper.Map<LhzxExpert>(input);
            await ExpertRepository.UpdateAsync(expert);
        }

        [AbpAuthorize]
        public async Task CreateExpert(CreateExpertInput input)
        {
            var expert = ObjectMapper.Map<LhzxExpert>(input);

            expert.OnlineStatus = (int)ExpertOnlineStatus.Offline;
            expert.IsExpert = true;

            await ExpertRepository.UpdateAsync(expert);
        }
    }

    public enum ExpertOnlineStatus
    {
        Offline = 1,
        Online = 2
    }

    public enum WeekDays
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5,
        Saturday = 6,
        Sunday = 7
    }

    public enum YearOfWorks
    {

    }
}
