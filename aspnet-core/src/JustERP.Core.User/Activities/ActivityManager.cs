using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Timing;
using JustERP.Core.User.Pepoles;
using Microsoft.EntityFrameworkCore;

namespace JustERP.Core.User.Activities
{
    public class ActivityManager : DomainService
    {
        private IRepository<MtLabel, long> _labelRepository;
        private IRepository<MtActivity, long> _activityRepository;
        private IRepository<MtPeopleActivity, long> _peopleActivityRepository;
        private IRepository<MtPeopleActivityLabel, long> _activityLabelRepository;
        public ActivityManager(IRepository<MtLabel, long> labelRepository,
            IRepository<MtActivity, long> activityRepository,
            IRepository<MtPeopleActivityLabel, long> activityLabelRepository,
            IRepository<MtPeopleActivity, long> peopleActivityRepository)
        {
            _labelRepository = labelRepository;
            _activityRepository = activityRepository;
            _peopleActivityRepository = peopleActivityRepository;
            _activityLabelRepository = activityLabelRepository;
        }

        public async Task InitLabels(MtPeople people)
        {
            var labelQuery = _labelRepository.GetAll().AsNoTracking();
            if (await labelQuery.AnyAsync(l => l.PeopleId == people.Id))
            {
                return;
            }
            var defaultLables = await labelQuery.Where(l => l.PeopleId == null).ToListAsync();
            foreach (var defaultLable in defaultLables)
            {
                defaultLable.Id = 0;
                defaultLable.PeopleId = people.Id;
                await _labelRepository.InsertAsync(defaultLable);
            }
        }

        public async Task InitActivities(MtPeople people)
        {
            var activityQuery = _activityRepository.GetAll().AsNoTracking();
            if (await activityQuery.AnyAsync(l => l.PeopleId == people.Id))
            {
                return;
            }
            var defaultActivities = await activityQuery.Where(l => l.IsSystem && l.IsDefault).ToListAsync();
            foreach (var defaultActivity in defaultActivities)
            {
                defaultActivity.Id = 0;
                defaultActivity.PeopleId = people.Id;
                defaultActivity.IsSystem = defaultActivity.IsDefault = false;
                await _activityRepository.InsertAsync(defaultActivity);
            }
        }

        public async Task<MtPeopleActivity> StartActivity(MtPeople people, MtActivity activity)
        {
            var currentActivities = await _peopleActivityRepository.GetAll()
                .Where(a => a.EndTime == null && a.PeopleId == people.Id)
                .ToListAsync();
            foreach (var currentActivity in currentActivities)
            {
                StopActivity(currentActivity);
            }
            var peopleActivity = new MtPeopleActivity
            {
                PeopleId = people.Id,
                ActivityIcon = activity.Icon,
                ActivityId = activity.Id,
                ActivityName = activity.Name,
                BeginTime = Clock.Now
            };
            await _peopleActivityRepository.InsertAsync(peopleActivity);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return peopleActivity;
        }

        public MtPeopleActivity StopActivity(MtPeopleActivity peopleActivity)
        {
            if (peopleActivity.EndTime.HasValue) return peopleActivity;

            peopleActivity.EndTime = Clock.Now;
            peopleActivity.TotalSeconds = peopleActivity.CalcTotalSeconds();

            return peopleActivity;
        }

        public async Task<MtActivity> AddActivity(MtPeople people, MtActivity activity)
        {
            activity.IsSystem = activity.IsDefault = false;
            activity.PeopleId = people.Id;
            activity = await _activityRepository.InsertAsync(activity);
            await UnitOfWorkManager.Current.SaveChangesAsync();
            return activity;
        }

        public Task DeleteActivity(MtActivity activity)
        {
            return _activityRepository.DeleteAsync(activity);
        }

        public Task DeleteLabel(MtLabel label)
        {
            return _labelRepository.DeleteAsync(label);
        }

        public async Task SetLabel(MtPeopleActivity peopleActivity, MtPeopleActivityLabel[] labels)
        {
            await _activityLabelRepository.DeleteAsync(l => l.PeopleActivityId == peopleActivity.Id);
            foreach (var label in labels)
            {
                label.PeopleActivityId = peopleActivity.Id;
                await _activityLabelRepository.InsertAsync(label);
                if (!await _labelRepository.GetAll()
                    .AnyAsync(l => l.PeopleId == peopleActivity.PeopleId && l.Name == label.LabelName))
                {
                    await _labelRepository.InsertAsync(new MtLabel
                    {
                        LabelCategoryId = label.LabelCategoryId,
                        Name = label.LabelName,
                        PeopleId = peopleActivity.PeopleId
                    });
                }
            }
        }
    }
}
