using System;
using System.Collections.Generic;
using System.Text;

namespace JustERP.Application.User.Peoples.Dto
{
    public class GetPeopleActivitiesInput
    {
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
