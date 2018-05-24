using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using JustERP.Application.User.Peoples.Dto;
using JustERP.Core.User.Activities;
using JustERP.Core.User.Pepoles;
using JustERP.Peoples;
using Microsoft.EntityFrameworkCore;

namespace JustERP.Application.User.Peoples
{
    [AbpAuthorize]
    public class PeopleAppService : ApplicationService, IPeopleAppService
    {
        private IRepository<MtPeopleActivity, long> _peopleActivityRepository;
        private IRepository<MtActivity, long> _activityRepository;
        private IRepository<MtPeople, long> _peopleRepository;
        private IRepository<MtLabel, long> _labelRepository;
        private IRepository<MtLabelCategory, long> _labelCategoryRepository;
        private IRepository<MtPeopleActivityLabel, long> _activityLabelRepository;

        private ActivityManager _activityManager;

        public PeopleAppService(
            IRepository<MtPeopleActivity, long> peopleActivityRepository,
            IRepository<MtActivity, long> activityRepository,
            IRepository<MtPeople, long> peopleRepository,
            IRepository<MtLabel, long> labelRepository,
            IRepository<MtLabelCategory, long> labelCategoryRepository,
            IRepository<MtPeopleActivityLabel, long> activityLabelRepository,
            ActivityManager activityManager)
        {
            _peopleActivityRepository = peopleActivityRepository;
            _activityRepository = activityRepository;
            _peopleRepository = peopleRepository;
            _activityManager = activityManager;
            _labelRepository = labelRepository;
            _labelCategoryRepository = labelCategoryRepository;
            _activityLabelRepository = activityLabelRepository;
        }

        public async Task<PeopleActivityDto> StartActivity(StartActivityInput input)
        {
            var activity = await _activityRepository.GetAsync(input.ActivityId);
            var people = await _peopleRepository.GetAsync(AbpSession.UserId.Value);

            var peopleActivity = await _activityManager.StartActivity(people, activity);

            return ObjectMapper.Map<PeopleActivityDto>(peopleActivity);
        }

        public async Task<PeopleActivityDto> StopActivity(StopActivityInput input)
        {
            var peopleActivity = await _peopleActivityRepository.GetAsync(input.PeopleActivityId);
            peopleActivity = _activityManager.StopActivity(peopleActivity);

            return ObjectMapper.Map<PeopleActivityDto>(peopleActivity);
        }

        public async Task<PeopleActivityDto> GetCurrentActivity()
        {
            var currentActivity = await _peopleActivityRepository
                .GetAllIncluding(a => a.PeopleActivityLabels)
                .FirstOrDefaultAsync(a => a.PeopleId == AbpSession.UserId && a.EndTime == null);

            return ObjectMapper.Map<PeopleActivityDto>(currentActivity);
        }

        public async Task<IList<PeopleActivityDto>> GetPeopleActivityHistory(GetActivityHistoryInput input)
        {
            var peopleActivities = QueryActivities(input);
            peopleActivities = peopleActivities.OrderByDescending(a => a.Id);
            var activityList = await peopleActivities.ToListAsync();

            activityList.Add(new MtPeopleActivity
            {
                TotalSeconds = input.GetTotalSeconds() - activityList.Sum(a => a.TotalSeconds),
                ActivityName = "未计时",
                BeginTime = input.BeginDate,
                EndTime = input.EndDate,
                PeopleActivityLabels = new List<MtPeopleActivityLabel>
                {
                    new MtPeopleActivityLabel{LabelCategoryId = 1,LabelName = "暂停"}
                }
            });

            return ObjectMapper.Map<IList<PeopleActivityDto>>(activityList);
        }

        private IQueryable<MtPeopleActivity> QueryActivities(GetActivityHistoryInput input)
        {
            var peopleActivities = _peopleActivityRepository
                .GetAllIncluding(a => a.PeopleActivityLabels)
                .Where(a => GetHistoryCondition(input, a));

            return peopleActivities;
        }

        private bool GetHistoryCondition(GetActivityHistoryInput input, MtPeopleActivity a)
        {
            return a.PeopleId == AbpSession.UserId &&
                   (a.BeginTime >= input.BeginDate &&
                    a.EndTime <= input.EndDate ||
                    a.BeginTime >= input.BeginDate && a.BeginTime <= input.EndDate && a.EndTime == null);
        }

        private async Task<IList<TotalActivityHistoryDto>> GetTotalActivityHistoryByLabel(GetActivityHistoryInput input)
        {
            var totalActivity = await _activityLabelRepository.GetAll().AsNoTracking()
                .Where(l => GetHistoryCondition(input, l.PeopleActivity))
                .GroupBy(l => new
                {
                    l.LabelName
                })
                .Select(g => new TotalActivityHistoryDto
                {
                    Remark = g.Key.LabelName,
                    TotalSeconds = g.Sum(l => l.PeopleActivity.TotalSeconds)
                }).ToListAsync();

            return totalActivity;
        }

        public async Task<IList<TotalActivityHistoryDto>> GetTotalActivityHistory(GetActivityHistoryInput input)
        {
            var peopleActivities = await QueryActivities(input)
                .GroupBy(a => new
                {
                    a.ActivityName,
                    a.ActivityIcon
                })
                .Select(g => new TotalActivityHistoryDto
                {
                    ActivityName = g.Key.ActivityName,
                    ActivityIcon = g.Key.ActivityIcon,
                    TotalSeconds = g.Sum(a => a.TotalSeconds)
                }).ToListAsync();

            return peopleActivities;
        }

        public async Task<ActivityDto> AddActivity(AddActivityInput input)
        {
            var people = await _peopleRepository.GetAsync(AbpSession.UserId.Value);
            var systemActivity = await _activityRepository.GetAll().AsNoTracking().SingleOrDefaultAsync(a => a.Id == input.ActivityId);
            systemActivity.Id = 0;
            systemActivity.Name = input.Name;
            systemActivity = await _activityManager.AddActivity(people, systemActivity);
            return ObjectMapper.Map<ActivityDto>(systemActivity);
        }

        public async Task DeleteActivity(long activityId)
        {
            var activity = await _activityRepository.GetAsync(activityId);
            if (activity.PeopleId != AbpSession.UserId) return;
            await _activityManager.DeleteActivity(activity);
        }

        [AbpAllowAnonymous]
        public async Task<IList<ActivityDto>> GetUsedActivities()
        {
            var usedActivities = await _activityRepository.GetAll()
                .Where(a => a.PeopleId == AbpSession.UserId)
                .OrderBy(a => a.Id)
                .ToListAsync();

            return ObjectMapper.Map<IList<ActivityDto>>(usedActivities);
        }

        public async Task<IList<ActivityDto>> GetSystemActivities()
        {
            var unUsedActivities = await _activityRepository.GetAll()
                .Where(a => a.IsSystem)
                .OrderBy(a => a.Id)
                .ToListAsync();

            return ObjectMapper.Map<IList<ActivityDto>>(unUsedActivities);
        }

        public async Task SetLabel(SetLabelInput input)
        {
            var peopleActivity = await _peopleActivityRepository.GetAsync(input.PeopleActivityId);
            var labels = ObjectMapper.Map<MtPeopleActivityLabel[]>(input.Labels);

            await _activityManager.SetLabel(peopleActivity, labels);
        }

        public async Task DeleteLabel(long labelId)
        {
            var label = await _labelRepository.GetAsync(labelId);
            if (label.PeopleId != AbpSession.UserId) return;
            await _activityManager.DeleteLabel(label);
        }

        public async Task<LabelCategoryDto> SetLabelCategoryName(SetLabelCategoryNameInput input)
        {
            var labelCategory = await _labelCategoryRepository.GetAsync(input.Id);
            labelCategory.SetPeopleName(AbpSession.UserId.Value, input.Name);
            return ObjectMapper.Map<LabelCategoryDto>(labelCategory);
        }

        public async Task<IList<LabelCategoryDto>> GetLabelCategories()
        {
            var labels = await _labelRepository
                .GetAllIncluding(l => l.LabelCategory)
                .Where(l => l.PeopleId == AbpSession.UserId)
                .ToListAsync();
            var groupLabels = labels.GroupBy(l => l.LabelCategory).Select(g => new LabelCategoryDto
            {
                Id = g.Key.Id,
                Name = g.Key.GetPeopleName(AbpSession.UserId.Value) ?? g.Key.Name,
                Labeles = ObjectMapper.Map<LabelDto[]>(g.ToArray())
            }).ToList();
            return groupLabels;
        }
    }
}
