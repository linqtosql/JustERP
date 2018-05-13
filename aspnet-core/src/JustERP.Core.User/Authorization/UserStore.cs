using System.Threading;
using System.Threading.Tasks;
using Abp.Dependency;
using JustERP.Core.User.Pepoles;
using Microsoft.AspNetCore.Identity;

namespace JustERP.Core.User.Authorization
{
    public class UserStore : IUserStore<MtPeople>, ITransientDependency
    {
        public void Dispose()
        {

        }

        public Task<string> GetUserIdAsync(MtPeople user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(MtPeople user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Openid);
        }

        public Task SetUserNameAsync(MtPeople user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedUserNameAsync(MtPeople user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(MtPeople user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(MtPeople user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(MtPeople user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(MtPeople user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<MtPeople> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<MtPeople> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
