using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using JustERP.Application.User.Peoples.Dto;
using JustERP.Core.User.Activities;
using JustERP.Peoples;

namespace JustERP.Application.User.Peoples
{
    [AbpAuthorize]
    public class PeopleAppService : ApplicationService, IPeopleAppService
    {
        private IRepository<MtPeopleActivity, long> _peopleActivityRepository;

        public PeopleAppService(IRepository<MtPeopleActivity, long> peopleActivityRepository)
        {
            _peopleActivityRepository = peopleActivityRepository;
        }

        public Task<PeopleActivityDto> StartActivity(StartActivityInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task<PeopleActivityDto> StopAndStartActivity(StopAndStartActivityInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task<PeopleActivityDto> StopActivity(long peopleActivityId)
        {
            throw new System.NotImplementedException();
        }

        public Task<PeopleActivityDto> GetCurrentActivity()
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<PeopleActivityDto>> GetPeopleActivities(GetPeopleActivitiesInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task<ActivityDto> AddActivity(AddActivityInput input)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteActivity(long activityId)
        {
            throw new System.NotImplementedException();
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
