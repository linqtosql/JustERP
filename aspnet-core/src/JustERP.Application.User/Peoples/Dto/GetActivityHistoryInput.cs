using System;
using Abp.Timing;

namespace JustERP.Application.User.Peoples.Dto
{
    public class GetActivityHistoryInput
    {
        private DateTypes _dateType = (DateTypes)(-1);
        public DateTypes DateType
        {
            get => _dateType;
            set
            {
                switch (value)
                {
                    case DateTypes.Today:
                        BeginDate = EndDate = Clock.Now;
                        break;
                    case DateTypes.Yesterday:
                        BeginDate = EndDate = Clock.Now.AddDays(-1);
                        break;
                    case DateTypes.ThisWeek:
                        BeginDate = Clock.Now.AddDays(-(Clock.Now.DayOfWeek == DayOfWeek.Sunday
                            ? 6
                            : (int)Clock.Now.DayOfWeek - 1));
                        EndDate = BeginDate.AddDays(6);
                        break;
                }
                _dateType = value;
            }
        }

        private DateTime _beginDate;

        public DateTime BeginDate
        {
            get => _beginDate;
            set => _beginDate = value.Date;
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                var val = value;
                _endDate = new DateTime(val.Year, val.Month, val.Day, 23, 59, 59);
            }
        }

        /// <summary>
        /// 获取开始与结束时间段内的总计秒数
        /// </summary>
        /// <returns></returns>
        public int GetTotalSeconds()
        {
            return (int)(EndDate - BeginDate).TotalSeconds;
        }

        public TotalActivityTypes? TotalType { get; set; }

        public enum DateTypes
        {
            Today,
            Yesterday,
            ThisWeek
        }
    }

    public enum TotalActivityTypes
    {
        Activity = 0,
        Label = 1,
        ActivityAndLabel = 4
    }
}
