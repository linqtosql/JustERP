using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using JustERP.Core.User.Pepoles;
using Microsoft.EntityFrameworkCore;

namespace JustERP.Core.User.Activities
{
    public class ActivityManager : DomainService
    {
        private IRepository<MtLabel, long> _labelRepository;
        private IRepository<MtActivity, long> _activityRepository;
        private IRepository<MtPeopleActivity, long> _peopleActivityRepository;
        public ActivityManager(IRepository<MtLabel, long> labelRepository,
            IRepository<MtActivity, long> activityRepository,
            IRepository<MtPeopleActivity, long> peopleActivityRepository)
        {
            _labelRepository = labelRepository;
            _activityRepository = activityRepository;
            _peopleActivityRepository = peopleActivityRepository;
        }

        public async void InitLabels(MtPeople people)
        {
            var labelQuery = _labelRepository.GetAll();
            if (await labelQuery.AnyAsync(l => l.PeopleId == people.Id))
            {
                return;
            }
            var defaultLables = await labelQuery.Where(l => l.PeopleId == null).ToListAsync();
            foreach (var defaultLable in defaultLables)
            {
                defaultLable.PeopleId = people.Id;
                await _labelRepository.InsertAsync(defaultLable);
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public async void InitActivities(MtPeople people)
        {
            var activityQuery = _activityRepository.GetAll();
            if (await activityQuery.AnyAsync(l => l.PeopleId == people.Id))
            {
                return;
            }
            var defaultActivities = await activityQuery.Where(l => l.IsSystem && l.IsDefault).ToListAsync();
            foreach (var defaultActivity in defaultActivities)
            {
                defaultActivity.PeopleId = people.Id;
                await _activityRepository.InsertAsync(defaultActivity);
            }
            await UnitOfWorkManager.Current.SaveChangesAsync();
        }

        public Task<MtPeopleActivity> StartActivity(MtPeople people, MtActivity activity)
        {
            return _peopleActivityRepository.InsertAsync(new MtPeopleActivity
            {
                PeopleId = people.Id,
                ActivityIcon = activity.Icon,
                ActivityId = activity.Id,
                ActivityName = activity.Name,
                BeginTime = DateTime.Now
            });
        }

        public MtPeopleActivity StopActivity(MtPeopleActivity peopleActivity)
        {
            peopleActivity.EndTime = DateTime.Now;
            peopleActivity.TotalSeconds = peopleActivity.CalcTotalSeconds();

            return peopleActivity;
        }

        public Task<MtActivity> AddActivity(MtPeople people, MtActivity activity)
        {
            activity.IsSystem = activity.IsDefault = false;
            activity.PeopleId = people.Id;
            return _activityRepository.InsertAsync(activity);
        }

        public Task DeleteActivity(MtActivity activity)
        {
            return _activityRepository.DeleteAsync(activity);
        }
    }
}
