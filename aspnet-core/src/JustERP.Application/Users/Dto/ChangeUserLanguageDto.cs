using System.ComponentModel.DataAnnotations;

namespace JustERP.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
