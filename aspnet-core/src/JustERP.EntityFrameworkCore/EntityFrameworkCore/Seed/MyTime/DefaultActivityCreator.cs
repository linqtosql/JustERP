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
            new MtActivity{Language = "en", Turn = 1,  IsSystem = true, IsDefault = false, Icon = "sb.png", Name = "On Duty"},
            new MtActivity{Language = "en", Turn = 2,  IsSystem = true, IsDefault = false, Icon = "bfkh.png", Name = "Client visit"},
            new MtActivity{Language = "en", Turn = 3,  IsSystem = true, IsDefault = false, Icon = "dhh.png", Name = "Tel conf"},
            new MtActivity{Language = "en", Turn = 4,  IsSystem = true, IsDefault = false, Icon = "ywhy.png", Name = "Biz meeting"},
            new MtActivity{Language = "en", Turn = 5,  IsSystem = true, IsDefault = false, Icon = "xzh.png", Name = "Team meeting"},
            new MtActivity{Language = "en", Turn = 6,  IsSystem = true, IsDefault = false, Icon = "yth.jpg", Name = "Seminar"},
            new MtActivity{Language = "en", Turn = 7,  IsSystem = true, IsDefault = false, Icon = "cc.png", Name = "Biz trip"},
            new MtActivity{Language = "en", Turn = 8,  IsSystem = true, IsDefault = false, Icon = "dy.png", Name = "Research"},
            new MtActivity{Language = "en", Turn = 9,  IsSystem = true, IsDefault = false, Icon = "fabj.png", Name = "Proposal"},
            new MtActivity{Language = "en", Turn = 10, IsSystem = true, IsDefault = false, Icon = "xb.png", Name = "Off Duty"},
            new MtActivity{Language = "en", Turn = 11, IsSystem = true, IsDefault = false, Icon = "xx.png", Name = "Break"},
            new MtActivity{Language = "en", Turn = 12, IsSystem = true, IsDefault = false, Icon = "yc.png", Name = "Meal"},
            new MtActivity{Language = "en", Turn = 13, IsSystem = true, IsDefault = false, Icon = "cx.png", Name = "Commute"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = false, Icon = "jh.png", Name = "Gathering"},
            new MtActivity{Language = "en", Turn = 15, IsSystem = true, IsDefault = false, Icon = "zdy.png", Name = "Undefined"},
            
            new MtActivity{Language = "zh-CN", Turn = 1, IsSystem = true, IsDefault = false, Icon = "sb.png", Name = "上班"},
            new MtActivity{Language = "zh-CN", Turn = 2, IsSystem = true, IsDefault = false, Icon = "bfkh.png", Name = "拜访客户"},
            new MtActivity{Language = "zh-CN", Turn = 3, IsSystem = true, IsDefault = false, Icon = "dhh.png", Name = "电话会"},
            new MtActivity{Language = "zh-CN", Turn = 4, IsSystem = true, IsDefault = false, Icon = "ywhy.png", Name = "业务会议"},
            new MtActivity{Language = "zh-CN", Turn = 5, IsSystem = true, IsDefault = false, Icon = "xzh.png", Name = "小组会"},
            new MtActivity{Language = "zh-CN", Turn = 6, IsSystem = true, IsDefault = false, Icon = "yth.jpg", Name = "研讨会"},
            new MtActivity{Language = "zh-CN", Turn = 7, IsSystem = true, IsDefault = false, Icon = "cc.png", Name = "出差"},
            new MtActivity{Language = "zh-CN", Turn = 8, IsSystem = true, IsDefault = false, Icon = "dy.png", Name = "调研"},
            new MtActivity{Language = "zh-CN", Turn = 9, IsSystem = true, IsDefault = false, Icon = "fabj.png", Name = "方案报价"},
            new MtActivity{Language = "zh-CN", Turn = 10, IsSystem = true, IsDefault = false, Icon = "xb.png", Name = "下班"},
            new MtActivity{Language = "zh-CN", Turn = 11, IsSystem = true, IsDefault = false, Icon = "xx.png", Name = "休息"},
            new MtActivity{Language = "zh-CN", Turn = 12, IsSystem = true, IsDefault = false, Icon = "yc.png", Name = "用餐"},
            new MtActivity{Language = "zh-CN", Turn = 13, IsSystem = true, IsDefault = false, Icon = "cx.png", Name = "出行"},
            new MtActivity{Language = "zh-CN", Turn = 14, IsSystem = true, IsDefault = false, Icon = "jh.png", Name = "聚会"},
            new MtActivity{Language = "zh-CN", Turn = 15, IsSystem = true, IsDefault = false, Icon = "zdy.png", Name = "自定义"}
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
            if (_context.Activities.IgnoreQueryFilters().Any(a =>
                a.IsSystem == activity.IsSystem &&
                a.IsDefault == activity.IsDefault &&
                a.Language == activity.Language &&
                a.Name == activity.Name)
            )
            {
                return;
            }

            _context.Activities.Add(activity);

            _context.SaveChanges();
        }
    }
}
