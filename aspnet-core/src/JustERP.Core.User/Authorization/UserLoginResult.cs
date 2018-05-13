using System.Security.Claims;
using Abp.Authorization;

namespace JustERP.Core.User.Authorization
{
    public class UserLoginResult
    {
        public UserLoginResult(AbpLoginResultType resultType, long userId = 0, ClaimsIdentity identity = null)
        {
            UserId = userId;
            Result = resultType;
            Identity = identity;
        }

        public UserLoginResult(long userId, ClaimsIdentity identity) : this(AbpLoginResultType.Success, userId, identity)
        {

        }

        public AbpLoginResultType Result { get; }
        public long UserId { get; }
        public ClaimsIdentity Identity { get; }
    }
}
