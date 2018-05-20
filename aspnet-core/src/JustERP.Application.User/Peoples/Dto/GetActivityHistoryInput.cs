using System;

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
                        BeginDate = EndDate = DateTime.Now;
                        break;
                    case DateTypes.Yesterday:
                        BeginDate = EndDate = DateTime.Now.AddDays(-1);
                        break;
                    case DateTypes.ThisWeek:
                        BeginDate = DateTime.Now.AddDays(-(DateTime.Now.DayOfWeek == DayOfWeek.Sunday
                            ? 6
                            : (int)DateTime.Now.DayOfWeek - 1));
                        EndDate = BeginDate.Value.AddDays(6);
                        break;
                }
                _dateType = value;
            }
        }

        private DateTime? _beginDate;

        public DateTime? BeginDate
        {
            get { return _beginDate; }
            set
            {
                if (value != null)
                {
                    _beginDate = value.Value.Date;
                }
            }
        }

        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value != null)
                {
                    var val = value.Value;
                    _endDate = new DateTime(val.Year, val.Month, val.Day, 23, 59, 59);
                }
            }
        }

        public enum DateTypes
        {
            Today,
            Yesterday,
            ThisWeek
        }
    }
}
