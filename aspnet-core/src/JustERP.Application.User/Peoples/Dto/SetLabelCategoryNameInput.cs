using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using JustERP.Core.User.Activities;

namespace JustERP.Application.User.Peoples.Dto
{
    [AutoMapTo(typeof(MtLabelCategory))]
    public class SetLabelCategoryNameInput : EntityDto<long>
    {
        public string Name { get; set; }
    }
}
