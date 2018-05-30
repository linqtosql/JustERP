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
            new MtActivity{Language = "zh-CN", Turn = 1, IsSystem = true, IsDefault = true, Icon = "bfkh.png", Name = "拜访客户"},
            new MtActivity{Language = "zh-CN", Turn = 2, IsSystem = true, IsDefault = true, Icon = "cc.png", Name = "出差"},
            new MtActivity{Language = "zh-CN", Turn = 3, IsSystem = true, IsDefault = true, Icon = "jfgz.jpg", Name = "计费工作"},
            new MtActivity{Language = "zh-CN", Turn = 4, IsSystem = true, IsDefault = true, Icon = "js.png", Name = "健身"},
            new MtActivity{Language = "zh-CN", Turn = 5, IsSystem = true, IsDefault = true, Icon = "skpx.jpg", Name = "上课培训"},
            new MtActivity{Language = "zh-CN", Turn = 6, IsSystem = true, IsDefault = true, Icon = "sj.png", Name = "睡觉"},
            new MtActivity{Language = "zh-CN", Turn = 7, IsSystem = true, IsDefault = true, Icon = "ywhy.png", Name = "业务会议"},


            new MtActivity{Language = "en", Turn = 8,  IsSystem = true, IsDefault = true, Icon = "Ballet.png", Name = "Ballet"},
            new MtActivity{Language = "en", Turn = 9,  IsSystem = true, IsDefault = true, Icon = "Bath.png", Name = "Bath"},
            new MtActivity{Language = "en", Turn = 10, IsSystem = true, IsDefault = false, Icon = "Breakfast.png", Name = "Breakfast"},
            new MtActivity{Language = "en", Turn = 11, IsSystem = true, IsDefault = false, Icon = "ChargeableHr.jpg", Name = "Chargeable Hr"},
            new MtActivity{Language = "en", Turn = 12, IsSystem = true, IsDefault = false, Icon = "ClientVisit.jpg", Name = "Client Visit"},
            new MtActivity{Language = "en", Turn = 13, IsSystem = true, IsDefault = true, Icon = "Debate.png", Name = "Debate"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = false, Icon = "Exam.png", Name = "Exam"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = false, Icon = "FastFood.png", Name = "Fast Food"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Gaming.png", Name = "Gaming"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Gardening.png", Name = "Gardening"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Grocery.png", Name = "Grocery"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = false, Icon = "HappyHour.png", Name = "Happy Hour"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Homework.png", Name = "Homework"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = false, Icon = "Housekeeping.png", Name = "Housekeeping"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Learning.png", Name = "Learning"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Phone.png", Name = "Phone"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "School.png", Name = "School"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "SocialMedia.png", Name = "Social Media"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = false, Icon = "SpaceTravel.png", Name = "Space Travel"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Supper.png", Name = "Supper"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = false, Icon = "teamwork.jpg", Name = "teamwork"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Video.png", Name = "Video"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Wash&Brush.png", Name = "Wash & Brush"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Workout.png", Name = "Workout"},
            new MtActivity{Language = "en", Turn = 14, IsSystem = true, IsDefault = true, Icon = "Yoga.png", Name = "Yoga"},


            new MtActivity{Language = "zh-CN",Turn = 8,  IsSystem = true, IsDefault = false, Icon = "Ballet.png", Name = "Ballet"},
            new MtActivity{Language = "zh-CN",Turn = 9,  IsSystem = true, IsDefault = false, Icon = "Bath.png", Name = "Bath"},
            new MtActivity{Language = "zh-CN",Turn = 10, IsSystem = true, IsDefault = false, Icon = "Breakfast.png", Name = "Breakfast"},
            new MtActivity{Language = "zh-CN",Turn = 11, IsSystem = true, IsDefault = false, Icon = "ChargeableHr.jpg", Name = "Chargeable Hr"},
            new MtActivity{Language = "zh-CN",Turn = 12, IsSystem = true, IsDefault = false, Icon = "ClientVisit.jpg", Name = "Client Visit"},
            new MtActivity{Language = "zh-CN",Turn = 13, IsSystem = true, IsDefault = false, Icon = "Debate.png", Name = "Debate"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Exam.png", Name = "Exam"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "FastFood.png", Name = "Fast Food"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Gaming.png", Name = "Gaming"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Gardening.png", Name = "Gardening"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Grocery.png", Name = "Grocery"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "HappyHour.png", Name = "Happy Hour"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Homework.png", Name = "Homework"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Housekeeping.png", Name = "Housekeeping"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Learning.png", Name = "Learning"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Phone.png", Name = "Phone"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "School.png", Name = "School"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "SocialMedia.png", Name = "Social Media"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "SpaceTravel.png", Name = "Space Travel"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Supper.png", Name = "Supper"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "teamwork.jpg", Name = "teamwork"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Video.png", Name = "Video"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Wash&Brush.png", Name = "Wash & Brush"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Workout.png", Name = "Workout"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "Yoga.png", Name = "Yoga"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "dxh.png", Name = "带小孩"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "dt.png", Name = "地铁"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "dhh.jpg", Name = "电话会"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "gtgm.png", Name = "赶头赶命"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "hj.png", Name = "回家"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "jgbs.png", Name = "机关办事"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "jb.png", Name = "加班"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "jlb.png", Name = "见老板"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "kt.png", Name = "开台"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "kzqs.png", Name = "看足球赛"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "khmc.png", Name = "客户名称"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "ly.png", Name = "淋浴"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "myf.png", Name = "买衣服"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "pyw.jpg", Name = "跑业务"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "qx.png", Name = "骑行"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "sb.png", Name = "上班"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "lg.png", Name = "溜狗"},
            new MtActivity{Language = "zh-CN",Turn = 14, IsSystem = true, IsDefault = false, Icon = "tlfb.png", Name = "头脑风暴"}
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
