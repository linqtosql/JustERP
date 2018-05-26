using System.Collections.Generic;
using System.Linq;
using JustERP.Core.User.Activities;
using Microsoft.EntityFrameworkCore;

namespace JustERP.EntityFrameworkCore.Seed.MyTime
{
    public class DefaultLabelCreator
    {
        private readonly JustERPDbContext _context;

        private static List<MtLabelCategory> InitialLabelCategories = new List<MtLabelCategory>
        {
            new MtLabelCategory
            {
                Id = 1,
                Name = "备注",
                //Labels = new []
                //{
                //    new MtLabel{LabelCategoryId = 1, Name = "海尔集团"},
                //    new MtLabel{LabelCategoryId = 1, Name = "香港贸发局"},
                //    new MtLabel{LabelCategoryId = 1, Name = "腾迅"},
                //    new MtLabel{LabelCategoryId = 1, Name = "宝山钢铁"},
                //    new MtLabel{LabelCategoryId = 1, Name = "丰田汽车"},
                //    new MtLabel{LabelCategoryId = 1, Name = "中国人寿"},
                //    new MtLabel{LabelCategoryId = 1, Name = "平安保险"},
                //    new MtLabel{LabelCategoryId = 1, Name = "三一重工"},
                //    new MtLabel{LabelCategoryId = 1, Name = "JP Morgan"},
                //    new MtLabel{LabelCategoryId = 1, Name = "中信泰富"},
                //    new MtLabel{LabelCategoryId = 1, Name = "Amazon"},
                //    new MtLabel{LabelCategoryId = 1, Name = "上海医药"},
                //    new MtLabel{LabelCategoryId = 1, Name = "麦肯锡"},
                //    new MtLabel{LabelCategoryId = 1, Name = "IBM"}
                //}
            },
            new MtLabelCategory
            {
                Id = 2,
                Name = "备注",
                //Labels = new []
                //{
                //    new MtLabel{LabelCategoryId = 2, Name = "年度审计"},
                //    new MtLabel{LabelCategoryId = 2, Name = "收购合并"},
                //    new MtLabel{LabelCategoryId = 2, Name = "争议调解"},
                //    new MtLabel{LabelCategoryId = 2, Name = "上市重组"},
                //    new MtLabel{LabelCategoryId = 2, Name = "常年顾问"},
                //    new MtLabel{LabelCategoryId = 2, Name = "技术调研"},
                //    new MtLabel{LabelCategoryId = 2, Name = "季度报表"},
                //    new MtLabel{LabelCategoryId = 2, Name = "行业情报"},
                //    new MtLabel{LabelCategoryId = 2, Name = "尽职调查"},
                //    new MtLabel{LabelCategoryId = 2, Name = "投标报价"},
                //    new MtLabel{LabelCategoryId = 2, Name = "系统咨询"},
                //    new MtLabel{LabelCategoryId = 2, Name = "特殊项目"},
                //    new MtLabel{LabelCategoryId = 2, Name = "薪酬激励"}
                //}
            }
        };
        public DefaultLabelCreator(JustERPDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLabelCategories();
        }

        private void CreateLabelCategories()
        {
            foreach (var category in InitialLabelCategories)
            {
                CreateLabelCategoryIfNotExists(category);
                if (category.Labels == null) continue;

                foreach (var label in category.Labels)
                {
                    CreateLabelIfNotExists(label);
                }
            }
        }

        void CreateLabelCategoryIfNotExists(MtLabelCategory category)
        {
            if (_context.LabelCategories.IgnoreQueryFilters().Any(a => a.Id == category.Id))
            {
                return;
            }

            _context.LabelCategories.Add(category);

            _context.SaveChanges();
        }

        void CreateLabelIfNotExists(MtLabel label)
        {
            if (_context.Labels.IgnoreQueryFilters().Any(a => a.LabelCategoryId == label.LabelCategoryId && a.Name == label.Name))
            {
                return;
            }

            _context.Labels.Add(label);

            _context.SaveChanges();
        }
    }
}
