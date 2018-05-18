using System.Collections.Generic;
using System.Linq;
using JustERP.Core.User.Activities;
using Microsoft.EntityFrameworkCore;

namespace JustERP.EntityFrameworkCore.Seed.MyTime
{
    public class DefaultActivityCreator
    {
        private readonly JustERPDbContext _context;

        private static List<MtActivity> InitialActivities = new List<MtActivity>
        {
            new MtActivity{Id = 1,  Turn = 1, Icon = "icon1.png", IsSystem = true, IsDefault = true, Name = "小米科技"},
            new MtActivity{Id = 2,  Turn = 2, Icon = "icon2.png", IsSystem = true, IsDefault = true, Name = "团队会"},
            new MtActivity{Id = 3,  Turn = 3, Icon = "icon3.png", IsSystem = true, IsDefault = true, Name = "收费工作"},
            new MtActivity{Id = 4,  Turn = 4, Icon = "icon4.png", IsSystem = true, IsDefault = true, Name = "电话会议"},
            new MtActivity{Id = 5,  Turn = 5, Icon = "icon5.png", IsSystem = true, IsDefault = true, Name = "Allen & Overy"},
            new MtActivity{Id = 6,  Turn = 6, Icon = "icon6.png", IsSystem = true, IsDefault = true, Name = "外出办事"},
            new MtActivity{Id = 7,  Turn = 7, Icon = "icon7.png", IsSystem = true, IsDefault = true, Name = "跑客户"},
            new MtActivity{Id = 8,  Turn = 8, Icon = "icon8.png", IsSystem = true, IsDefault = true, Name = "培训"},
            new MtActivity{Id = 9,  Turn = 9, Icon = "icon9.png", IsSystem = true, IsDefault = true, Name = "Li&Fung"},
            new MtActivity{Id = 10, Turn = 10,Icon = "icon10.png",  IsSystem = true, IsDefault = true, Name = "西安杨森"},
            new MtActivity{Id = 11, Turn = 11,Icon = "icon11.png",  IsSystem = true, IsDefault = true, Name = "充电"},
            new MtActivity{Id = 12, Turn = 12,Icon = "icon12.png",  IsSystem = true, IsDefault = true, Name = "地铁"},
            new MtActivity{Id = 13, Turn = 13,Icon = "icon13.png",  IsSystem = true, IsDefault = true, Name = "吃饭"},
            new MtActivity{Id = 14, Turn = 14,Icon = "icon14.png",  IsSystem = true, IsDefault = true, Name = "睡觉"},

            new MtActivity{Name = "小米科技"},
        };

        public DefaultActivityCreator(JustERPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateActivities();
        }

        private void CreateActivities()
        {
            foreach (var activity in InitialActivities)
            {
                CreateActivityIfNotExists(activity);
            }
        }

        private void CreateActivityIfNotExists(MtActivity activity)
        {
            if (_context.Activities.IgnoreQueryFilters().Any(a => a.IsSystem && a.IsDefault && a.Name == activity.Name))
            {
                return;
            }

            _context.Activities.Add(activity);

            _context.SaveChanges();
        }
    }
}
