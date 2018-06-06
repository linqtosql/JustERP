namespace JustERP.Core.User.Authorization
{
    public class LoginModel
    {
        public string OpenId { get; set; }
        public string AvatarUrl { get; set; }
        public string NickName { get; set; }
        public int TimezoneOffset { get; set; }
    }
}
