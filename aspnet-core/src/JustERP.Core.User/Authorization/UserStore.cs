using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using JustERP.Core.User.Experts;
using Microsoft.AspNetCore.Identity;

namespace JustERP.Core.User
{
    public class UserStore : IUserStore<LhzxExpert>, ITransientDependency
    {
        public void Dispose()
        {

        }

        public Task<string> GetUserIdAsync(LhzxExpert user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(LhzxExpert user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Phone);
        }

        public Task SetUserNameAsync(LhzxExpert user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(LhzxExpert user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(LhzxExpert user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(LhzxExpert user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(LhzxExpert user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(LhzxExpert user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<LhzxExpert> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<LhzxExpert> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
