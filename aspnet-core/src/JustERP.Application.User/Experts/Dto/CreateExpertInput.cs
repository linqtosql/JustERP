using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Abp.AutoMapper;
using Abp.Runtime.Validation;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMap(typeof(LhzxExpert))]
    public class CreateExpertInput : CreateNonExpertInput, ICustomValidate
    {
        [Required(ErrorMessage = "请选择您想成为的专家分类")]
        public override int? ExpertType { get; set; }
        [Required(ErrorMessage = "请选择您所在的领域")]
        public override long? ExpertFirstClassId { get; set; }
        [Required(ErrorMessage = "请选择您所在的细分领域")]
        public override long? ExpertClassId { get; set; }
        [Required(ErrorMessage = "请输入您所在的任职机构")]
        [MaxLength(32)]
        public override string Organization { get; set; }
        [Required(ErrorMessage = "请填写您的职位/专业")]
        [MaxLength(32)]
        public override string Post { get; set; }
        [Required(ErrorMessage = "请选择您的工作年限")]
        public override int? WorkYears { get; set; }

        [MaxLength(64)]
        public string Tags { get; set; }
        [Required(ErrorMessage = "请输入您的个人介绍")]
        [MaxLength(512)]
        public string Introduction { get; set; }
        [Required(ErrorMessage = "请输入您的专家介绍")]
        [MaxLength(512)]
        public string Speciality { get; set; }
        [Required(ErrorMessage = "请输入您每节的咨询价格")]
        public decimal? Price { get; set; }
        [MaxLength(32)]
        public string AlipayAccount { get; set; }
        [MaxLength(32)]
        public string WeixinAccount { get; set; }
        
        public List<CreateExpertWorkSettingInput> ExpertWorkSettings { get; set; }
        
        public List<string> ExpertPhotos { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (ExpertPhotos == null || ExpertPhotos.Count == 0 || ExpertPhotos.All(String.IsNullOrWhiteSpace))
            {
                context.Results.Add(new ValidationResult("请上传您的个人介绍图片"));
            }
            if (ExpertWorkSettings == null || ExpertWorkSettings.Count == 0 ||
                ExpertWorkSettings.All(w => w.EndTime == null || w.StartTime == null || w.Week < 0))
            {
                context.Results.Add(new ValidationResult("请设置您的营业时间"));
            }
        }
    }
}
