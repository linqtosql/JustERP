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
            set => _dateType = value;
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

        private DateTime _clientDateTime;
        public DateTime ClientDateTime
        {
            get { return _clientDateTime; }
            set
            {
                _clientDateTime = value;
                switch (DateType)
                {
                    case DateTypes.Today:
                        BeginDate = EndDate = _clientDateTime;
                        break;
                    case DateTypes.Yesterday:
                        BeginDate = EndDate = _clientDateTime.AddDays(-1);
                        break;
                    case DateTypes.ThisWeek:
                        BeginDate = _clientDateTime.AddDays(-(_clientDateTime.DayOfWeek == DayOfWeek.Sunday
                            ? 6
                            : (int)Clock.Now.DayOfWeek - 1));
                        EndDate = BeginDate.AddDays(6);
                        break;
                }
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

        public HistoryOrderBy? OrderBy { get; set; }

        public enum DateTypes
        {
            Today,
            Yesterday,
            ThisWeek
        }
    }

    public enum HistoryOrderBy
    {
        ActivityName = 0,
        Label = 1
    }

    public enum TotalActivityTypes
    {
        Activity = 0,
        Label = 1,
        LabelAndActivity = 3,
        ActivityAndLabel = 4
    }
}
