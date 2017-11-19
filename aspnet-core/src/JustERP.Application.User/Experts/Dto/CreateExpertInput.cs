using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMapTo(typeof(LhzxExpert))]
    public class CreateExpertInput : CreateNonExpertInput
    {
        [Required(ErrorMessage = "请选择您想成为的专家分类")]
        public int ExpertType { get; set; }
        [MaxLength(64)]
        public string Tags { get; set; }
        [Required(ErrorMessage = "请输入您的个人介绍")]
        [MaxLength(512)]
        public string Introduction { get; set; }
        [Required(ErrorMessage = "请输入您的专家介绍")]
        [MaxLength(512)]
        public string Speciality { get; set; }
        [Required(ErrorMessage = "请输入您每节的咨询价格")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "请输入您的支付宝收款账号")]
        [MaxLength(32)]
        public string AlipayAccount { get; set; }
        [Required(ErrorMessage = "请输入您的微信收款账号")]
        [MaxLength(32)]
        public string WeixinAccount { get; set; }
        [Required(ErrorMessage = "请设置您的营业时间")]
        public List<CreateExpertWorkSettingInput> ExpertWorkSettings { get; set; }
        [Required(ErrorMessage = "请上传您的个人介绍图片")]
        public List<string> ExpertPhotos { get; set; }
    }
}
