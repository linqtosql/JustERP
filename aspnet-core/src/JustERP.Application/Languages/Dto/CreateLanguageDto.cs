using Abp.AutoMapper;
using Abp.Localization;

namespace JustERP.Languages.Dto
{
    [AutoMapTo(typeof(ApplicationLanguage))]
    public class CreateLanguageDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }

        public string Icon { get; set; }
        public bool IsDisabled { get; set; }
    }
}
