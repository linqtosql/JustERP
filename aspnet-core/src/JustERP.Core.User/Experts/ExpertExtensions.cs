using System.Linq;
using Abp.Timing;

namespace JustERP.Core.User.Experts
{
    public static class ExpertExtensions
    {
        public static ExpertOnlineStatus GetOnlineStatus(this LhzxExpert expert)
        {
            if (!expert.IsExpert) return ExpertOnlineStatus.Offline;

            var workSettings = expert.ExpertWorkSettings;
            var week = Clock.Now.DayOfWeek;
            if (workSettings.Any(
                w => w.Week == (int)week &&
                w.StartTime.TimeOfDay <= Clock.Now.TimeOfDay &&
                w.EndTime.TimeOfDay >= Clock.Now.TimeOfDay)) return ExpertOnlineStatus.Online;

            return ExpertOnlineStatus.Offline;
        }
    }
}
