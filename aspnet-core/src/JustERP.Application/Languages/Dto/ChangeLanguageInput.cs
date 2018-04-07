using System.ComponentModel.DataAnnotations;

namespace JustERP.Languages.Dto
{
    public class ChangeLanguageInput
    {
        public ChangeLanguageInput(int? tenantId, string languageName)
        {
            TenantId = tenantId;
            LanguageName = languageName;
        }

        public int? TenantId { get; set; }
        [Required]
        public string LanguageName { get; set; }
    }
}