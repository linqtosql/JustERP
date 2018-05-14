using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using JustERP.Application.User.Peoples.Dto;
using JustERP.Peoples;

namespace JustERP.Application.User.Peoples
{
    public class PeopleAppService : ApplicationService, IPeopleAppService
    {
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
    }
}
