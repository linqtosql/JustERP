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
        public IRepository<LhzxExpertFriendShip, long> ExpertFriendRepository { get; set; }
        public IRepository<LhzxExpertAnonymousShip, long> AnonymousFriendRepository { get; set; }
        public IRepository<LhzxExpertWorkSetting, long> WorkSettingRepository { get; set; }
        public IRepository<LhzxExpertComment, long> CommentRepository { get; set; }
        public IAsyncQueryableExecuter AsyncQueryableExecuter { get; set; }
        private ExpertManager _expertManager;

        public ExpertAppService(ExpertManager expertManager)
        {
            _expertManager = expertManager;
        }

        public async Task<List<ExpertClassDto>> GetGroupedByClassExperts()
        {
            var query = ExpertClassRepository.GetAllIncluding(c => c.Experts)
                .Where(c => c.ParentId != null);
            var list = await AsyncQueryableExecuter.ToListAsync(query);
            foreach (var lhzxExpertClass in list)
            {
                lhzxExpertClass.Experts = lhzxExpertClass.Experts.Where(e => e.IsExpert).ToList();
            }
            return ObjectMapper.Map<List<ExpertClassDto>>(list);
        }

        public async Task<ExpertDetailsDto> GetExpertDetail(GetExpertDetailInput input)
        {
            if (!input.Id.HasValue && !input.ExpertAccountId.HasValue)
                throw new UserFriendlyException("Id或者ExpertAccountId必须有一个大于0");
            var expert = await ExpertRepository.GetAllIncluding(
                e => e.ExpertClass,
                e => e.ExpertFirstClass,
                e => e.ExpertWorkSettings)
                .SingleOrDefaultAsync(e => input.Id > 0 ? e.Id == input.Id : e.ExpertAccountId == input.ExpertAccountId);

            var expertDto = ObjectMapper.Map<ExpertDetailsDto>(expert);

            return expertDto;
        }

        public async Task<List<ExpertCommentDto>> GetExpertComments(long expertId)
        {
            var expertComments = await CommentRepository
                .GetAllIncluding(e => e.CommenterExpert)
                .Where(e => e.ExpertId == expertId).ToListAsync();
            return ObjectMapper.Map<List<ExpertCommentDto>>(expertComments);
        }

        [AbpAuthorize]
        public async Task<CreateNonExpertInput> GetNonExpert()
        {
            var expert = await ExpertRepository.GetAsync(AbpSession.UserId.Value);

            return ObjectMapper.Map<CreateNonExpertInput>(expert);
        }

        [AbpAuthorize]
        public async Task<CreateExpertInput> GetExpert()
        {
            var expert = await ExpertRepository.GetAllIncluding(
                e => e.ExpertWorkSettings).SingleOrDefaultAsync(e => e.Id == AbpSession.UserId);

            return ObjectMapper.Map<CreateExpertInput>(expert);
        }

        public async Task<List<ExpertClassDto>> GetAllExpertClasses()
        {
            var list = await ExpertClassRepository.GetAllIncluding(
                e => e.ChildrenExpertClasses)
                .Where(e => e.ParentId == null).ToListAsync();
            return ObjectMapper.Map<List<ExpertClassDto>>(list);
        }

        public async Task<List<ExpertDto>> GetExperts(SearchExpertInput input)
        {
            var query = ExpertRepository.GetAllIncluding(e => e.ExpertFirstClass, e => e.ExpertClass)
                .Where(e => e.IsExpert)
                .Where(e =>
            e.Name.Contains(input.Keyword) ||
            e.ExpertClass.Name.Contains(input.Keyword) ||
            e.ExpertFirstClass.Name.Contains(input.Keyword));

            var list = await query.ToListAsync();
            return ObjectMapper.Map<List<ExpertDto>>(list);
        }

        [AbpAuthorize]
        public async Task<LoggedInExpertOutput> GetExpertLoginInfo()
        {
            if (!AbpSession.UserId.HasValue) throw new UserFriendlyException("当前用户未登录");

            var expert = await ExpertRepository.SingleAsync(e => e.ExpertAccountId == AbpSession.UserId);
            return ObjectMapper.Map<LoggedInExpertOutput>(expert);
        }

        [AbpAuthorize]
        public async Task CreateNonExpert(CreateNonExpertInput input)
        {
            var expert = await ExpertRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, expert);

            await ExpertRepository.UpdateAsync(expert);
        }

        [AbpAuthorize]
        public async Task CreateExpert(CreateExpertInput input)
        {
            if (input.ExpertWorkSettings.GroupBy(w => w.Week).Any(g => g.Count() > 1))
            {
                throw new UserFriendlyException("营业时间同一天只能选择一次");
            }
            var expert = await ExpertRepository.GetAsync(input.Id);
            ObjectMapper.Map(input, expert);

            expert.OnlineStatus = expert.OnlineStatus ?? (int)ExpertOnlineStatus.Offline;
            expert.IsExpert = true;

            foreach (var workSetting in expert.ExpertWorkSettings)
            {
                if (workSetting.Id > 0)
                {
                    await WorkSettingRepository.UpdateAsync(workSetting);
                }
                else
                {
                    await WorkSettingRepository.InsertAsync(workSetting);
                }
            }

            await ExpertRepository.UpdateAsync(expert);
        }

        public async Task<ExpertPriceDto> GetExpertPrice(long expertId)
        {
            var expert = await ExpertRepository.GetAsync(expertId);
            return ObjectMapper.Map<ExpertPriceDto>(expert);
        }

        [AbpAuthorize]
        public async Task<ExpertDto> ChangeOnlineStatusTo(ChangeOnlineStatusInput input)
        {
            var expert = await ExpertRepository.GetAsync(AbpSession.UserId.Value);
            expert.OnlineStatus = (int)input.OnlineStatus;
            await ExpertRepository.UpdateAsync(expert);

            return ObjectMapper.Map<ExpertDto>(expert);
        }

        [AbpAuthorize]
        public async Task<ExpertFriendDto> CreateExpertFriend(CreateExpertFriendDto input)
        {
            var anonym = ObjectMapper.Map<LhzxExpertAnonymousShip>(input);
            var expert = ExpertRepository.GetAsync(input.ExpertId);
            var friendExpert = ExpertRepository.FirstOrDefaultAsync(e => e.ExpertAccount.UserName == input.UserName);
            var result = await Task.WhenAll(expert, friendExpert);
            if (result[1] == null)
            {
                return ObjectMapper.Map<ExpertFriendDto>(await _expertManager.CreateExpertFriend(result[0], anonym));
            }
            return ObjectMapper.Map<ExpertFriendDto>(await _expertManager.CreateExpertFriend(result[0], result[1]));
        }

        [AbpAuthorize]
        public async Task<List<ExpertFriendDto>> GetExpertFriends()
        {
            var expertFriends = await ExpertFriendRepository.GetAllIncluding(
                f => f.ExpertFriend,
                f => f.ExpertFriend.ExpertAccount)
                .Where(f => f.ExpertId == AbpSession.UserId).ToListAsync();
            var anonymousFriends = await AnonymousFriendRepository.GetAllListAsync(f => f.ExpertId == AbpSession.UserId);

            var friends = ObjectMapper.Map<List<ExpertFriendDto>>(expertFriends);
            friends.AddRange(ObjectMapper.Map<List<ExpertFriendDto>>(anonymousFriends));
            return friends;
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
