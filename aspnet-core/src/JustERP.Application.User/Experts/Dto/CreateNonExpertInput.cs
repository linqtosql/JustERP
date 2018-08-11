using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Experts;

namespace JustERP.Application.User.Experts.Dto
{
    [AutoMap(typeof(LhzxExpert))]
    public class CreateNonExpertInput : IEntityDto<long>
    {
        public virtual int? ExpertType { get; set; }
        public virtual long? ExpertFirstClassId { get; set; }
        public virtual long? ExpertClassId { get; set; }
        [Required(ErrorMessage = "请输入您的手机号")]
        [MaxLength(16)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "请输入您的姓名")]
        [MaxLength(16)]
        public string Name { get; set; }
        [MaxLength(128)]
        public string BackgroundImage { get; set; }
        [MaxLength(32)]
        public virtual string Organization { get; set; }
        [MaxLength(32)]
        public virtual string Post { get; set; }
        public virtual int? WorkYears { get; set; }

        public long Id { get; set; }
    }
}
