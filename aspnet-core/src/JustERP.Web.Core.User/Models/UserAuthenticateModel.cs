using System.ComponentModel.DataAnnotations;

namespace JustERP.Web.Core.User.Models
{
    public class UserAuthenticateModel
    {
        [Required]
        [MaxLength(13)]
        public string Phone { get; set; }

        [MaxLength(4)]
        public string PhoneCode { get; set; }

        public bool RememberClient { get; set; }
    }
}
