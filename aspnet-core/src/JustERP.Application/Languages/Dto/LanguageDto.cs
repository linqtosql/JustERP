using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Localization;

namespace JustERP.Languages.Dto
{
    [AutoMapFrom(typeof(ApplicationLanguage))]
    public class LanguageDto : EntityDto<int>
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public string Icon { get; set; }
        public bool IsDisabled { get; set; }
    }
}
