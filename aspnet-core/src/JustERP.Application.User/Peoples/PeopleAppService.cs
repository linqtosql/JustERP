using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.Timing;
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
        private const string LabelSeparator = " , ";
        private IRepository<MtPeopleActivity, long> _peopleActivityRepository;
        private IRepository<MtActivity, long> _activityRepository;
        private IRepository<MtPeople, long> _peopleRepository;
        private IRepository<MtLabel, long> _labelRepository;
        private IRepository<MtLabelCategory, long> _labelCategoryRepository;

        private ActivityManager _activityManager;

        public PeopleAppService(
            IRepository<MtPeopleActivity, long> peopleActivityRepository,
            IRepository<MtActivity, long> activityRepository,
            IRepository<MtPeople, long> peopleRepository,
            IRepository<MtLabel, long> labelRepository,
            IRepository<MtLabelCategory, long> labelCategoryRepository,
            ActivityManager activityManager)
        {
            _peopleActivityRepository = peopleActivityRepository;
            _activityRepository = activityRepository;
            _peopleRepository = peopleRepository;
            _activityManager = activityManager;
            _labelRepository = labelRepository;
            _labelCategoryRepository = labelCategoryRepository;
            
            LocalizationSourceName = "JustERP";
        }

        public async Task<PeopleActivityDto> StartActivity(StartActivityInput input)
        {
            var activity = await _activityRepository.GetAsync(input.ActivityId);
            var people = await _peopleRepository.GetAsync(AbpSession.GetUserId());

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
                .FirstOrDefaultAsync(a => a.PeopleId == AbpSession.GetUserId() && a.EndTime == null);

            return ObjectMapper.Map<PeopleActivityDto>(currentActivity);
        }

        public async Task<IList<PeopleActivityDto>> GetPeopleActivityHistory(GetActivityHistoryInput input)
        {
            var peopleActivities = QueryActivities(input);
            peopleActivities = peopleActivities.OrderByDescending(a => a.Id);
            var activityList = await peopleActivities.ToListAsync();

            if (!input.TotalType.HasValue && input.BeginDate.Date != Clock.Now.Date)
            {
                activityList.Add(GetUnTimingTotal(input, activityList));
            }

            if (input.TotalType.HasValue)
            {
                return GetTotalActivityHistory(activityList, input.TotalType.Value);
            }

            return ObjectMapper.Map<IList<PeopleActivityDto>>(activityList);
        }

        private MtPeopleActivity GetUnTimingTotal(GetActivityHistoryInput input, List<MtPeopleActivity> activityList)
        {
            var totalSecond = input.GetTotalSeconds();

            var peopleSecond = activityList.Sum(a => a.TotalSeconds);
            return new MtPeopleActivity
            {
                TotalSeconds = totalSecond - peopleSecond,
                ActivityName = L("TimeNotRecorded"),
                BeginTime = input.BeginDate,
                EndTime = input.EndDate,
                PeopleActivityLabels = new List<MtPeopleActivityLabel>
                {
                    new MtPeopleActivityLabel{LabelCategoryId = 1,LabelName = L("Paused")}
                }
            };
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
            return a.PeopleId == AbpSession.GetUserId() &&
                   (a.BeginTime >= input.BeginDate &&
                    a.EndTime <= input.EndDate ||
                    a.BeginTime >= input.BeginDate && a.BeginTime <= input.EndDate && a.EndTime == null);
        }

        private IList<PeopleActivityDto> GetTotalActivityHistory(IList<MtPeopleActivity> peopleActivities, TotalActivityTypes totalType)
        {
            IEnumerable<PeopleActivityDto> totalByLabel = new List<PeopleActivityDto>();
            switch (totalType)
            {
                case TotalActivityTypes.Activity:
                    totalByLabel = peopleActivities.GroupBy(a => new
                    {
                        a.ActivityName
                    })
                    .Select(g => new PeopleActivityDto
                    {
                        ActivityName = g.Key.ActivityName,
                        TotalSeconds = g.Sum(a => a.TotalSeconds)
                    });
                    break;
                case TotalActivityTypes.Label:
                    totalByLabel = peopleActivities.GroupBy(a => new
                    {
                        LabelName = a.PeopleActivityLabels.Select(l => l.LabelName).JoinAsString(LabelSeparator)
                    })
                    .Select(g => new PeopleActivityDto
                    {
                        Remark = g.Key.LabelName,
                        TotalSeconds = g.Sum(a => a.TotalSeconds)
                    });
                    break;
                case TotalActivityTypes.ActivityAndLabel:
                    totalByLabel = peopleActivities.GroupBy(a => new
                    {
                        a.ActivityName,
                        LabelName = a.PeopleActivityLabels.Select(l => l.LabelName).JoinAsString(LabelSeparator)
                    })
                    .Select(g => new PeopleActivityDto
                    {
                        ActivityName = g.Key.ActivityName,
                        Remark = g.Key.LabelName,
                        TotalSeconds = g.Sum(a => a.TotalSeconds)
                    });
                    break;
            }

            return totalByLabel.OrderByDescending(a => a.TotalSeconds).ToList();
        }

        public async Task<ActivityDto> AddActivity(AddActivityInput input)
        {
            var people = await _peopleRepository.GetAsync(AbpSession.GetUserId());
            var systemActivity = await _activityRepository.GetAll().AsNoTracking().SingleOrDefaultAsync(a => a.Id == input.ActivityId);
            systemActivity.Id = 0;
            systemActivity.Name = input.Name;
            systemActivity = await _activityManager.AddActivity(people, systemActivity);
            return ObjectMapper.Map<ActivityDto>(systemActivity);
        }

        public async Task DeleteActivity(long activityId)
        {
            var activity = await _activityRepository.GetAsync(activityId);
            if (activity.PeopleId != AbpSession.GetUserId()) return;
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
            if (label.PeopleId != AbpSession.GetUserId()) return;
            await _activityManager.DeleteLabel(label);
        }

        public async Task<LabelCategoryDto> SetLabelCategoryName(SetLabelCategoryNameInput input)
        {
            var labelCategory = await _labelCategoryRepository.GetAsync(input.Id);
            labelCategory.SetPeopleName(AbpSession.GetUserId(), input.Name);
            return ObjectMapper.Map<LabelCategoryDto>(labelCategory);
        }

        public async Task<IList<LabelCategoryDto>> GetLabelCategories()
        {
            var labelCategoryDto = await (from c in _labelCategoryRepository.GetAll()
                                          select new LabelCategoryDto
                                          {
                                              Id = c.Id,
                                              Name = c.GetPeopleName(AbpSession.GetUserId()) ?? c.Name
                                          }).ToListAsync();
            foreach (var categoryDto in labelCategoryDto)
            {
                var labels = from l in _labelRepository.GetAll()
                             where l.LabelCategoryId == categoryDto.Id && l.PeopleId == AbpSession.GetUserId()
                             select l;
                categoryDto.Labeles = ObjectMapper.Map<LabelDto[]>(labels);
            }
            return labelCategoryDto;
        }
    }
}
