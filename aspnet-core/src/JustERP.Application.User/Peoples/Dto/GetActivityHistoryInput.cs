using System;

namespace JustERP.Application.User.Peoples.Dto
{
    public class GetActivityHistoryInput
    {
        public DateTypes DateType { get; set; }
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

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
