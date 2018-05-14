using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapFrom(typeof(MtLabel))]
    public class LabelDto : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
