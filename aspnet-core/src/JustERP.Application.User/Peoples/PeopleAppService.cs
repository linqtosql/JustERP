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

        private ActivityManager _activityManager;

        public PeopleAppService(
            IRepository<MtPeopleActivity, long> peopleActivityRepository,
            IRepository<MtActivity, long> activityRepository,
            IRepository<MtPeople, long> peopleRepository,
            ActivityManager activityManager)
        {
            _peopleActivityRepository = peopleActivityRepository;
            _activityRepository = activityRepository;
            _peopleRepository = peopleRepository;
            _activityManager = activityManager;
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

            if (input.ActivityId.HasValue)
            {
                await StartActivity(new StartActivityInput { ActivityId = input.ActivityId.Value });
            }

            return ObjectMapper.Map<PeopleActivityDto>(peopleActivity);
        }

        public async Task<PeopleActivityDto> GetCurrentActivity()
        {
            var currentActivity = await _peopleActivityRepository.FirstOrDefaultAsync(a => a.EndTime == null);

            return ObjectMapper.Map<PeopleActivityDto>(currentActivity);
        }

        public async Task<IList<PeopleActivityDto>> GetPeopleActivities(GetPeopleActivitiesInput input)
        {
            var peopleActivities = await
                _peopleActivityRepository
                    .GetAllIncluding(a => a.People, a => a.PeopleActivityLabels)
                    .Where(a => a.PeopleId == AbpSession.UserId)
                    .ToListAsync();

            return ObjectMapper.Map<IList<PeopleActivityDto>>(peopleActivities);
        }

        public async Task<ActivityDto> AddActivity(AddActivityInput input)
        {
            var people = await _peopleRepository.GetAsync(AbpSession.UserId.Value);
            var systemActivity = await _activityRepository.GetAsync(input.ActivityId);
            var addActivity = ObjectMapper.Map<MtActivity>(systemActivity);
            addActivity.Name = input.Name;
            addActivity = await _activityManager.AddActivity(people, addActivity);
            return ObjectMapper.Map<ActivityDto>(addActivity);
        }

        public async Task DeleteActivity(long activityId)
        {
            var activity = await _activityRepository.GetAsync(activityId);
            await _activityManager.DeleteActivity(activity);
        }

        [AbpAllowAnonymous]
        public Task<IList<ActivityDto>> GetUsedActivities()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<ActivityDto>> GetUnUsedActivities()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<ActivityLabelDto>> SetLabel(SetLabelInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task<LabelCategoryDto> SetLabelCategoryName(SetLabelCategoryNameInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<LabelCategoryDto>> GetLabelCategories()
        {
            throw new System.NotImplementedException();
        }
    }
}
