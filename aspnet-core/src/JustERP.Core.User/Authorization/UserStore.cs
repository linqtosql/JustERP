using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using JustERP.Core.User.Experts;
using Microsoft.AspNetCore.Identity;

namespace JustERP.Core.User
{
    public class UserStore : IUserStore<LhzxExpertAccount>, ITransientDependency
    {
        public void Dispose()
        {

        }

        public Task<string> GetUserIdAsync(LhzxExpertAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(LhzxExpertAccount user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(LhzxExpertAccount user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(LhzxExpertAccount user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(LhzxExpertAccount user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(LhzxExpertAccount user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(LhzxExpertAccount user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(LhzxExpertAccount user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<LhzxExpertAccount> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<LhzxExpertAccount> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
