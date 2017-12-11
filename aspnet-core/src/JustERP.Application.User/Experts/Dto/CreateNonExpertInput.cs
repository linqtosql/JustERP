using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMap(typeof(LhzxExpert))]
    public class CreateNonExpertInput : IEntityDto<long>
    {
        [Required(ErrorMessage = "请选择您所在的领域")]
        public long ExpertFirstClassId { get; set; }
        [Required(ErrorMessage = "请选择您所在的细分领域")]
        public long ExpertClassId { get; set; }
        [Required(ErrorMessage = "请输入您的手机号")]
        [MaxLength(16)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "请输入您的姓名")]
        [MaxLength(16)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string Avatar { get; set; }
        [MaxLength(128)]
        public string BackgroundImage { get; set; }
        [Required(ErrorMessage = "请输入您所在的任职机构")]
        [MaxLength(32)]
        public string Organization { get; set; }
        [Required(ErrorMessage = "请填写您的职位/专业")]
        [MaxLength(32)]
        public string Post { get; set; }
        [Required(ErrorMessage = "请选择您的工作年限")]
        public int WorkYears { get; set; }

        public long Id { get; set; }
    }
}
