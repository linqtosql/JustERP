using System;
using System.Threading.Tasks;
using Abp;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JustERP.Core.User.Experts
{
    public class ExpertManager : UserManager<LhzxExpertAccount>, IDomainService
    {
        private IRepository<LhzxExpertAccount, long> _expertAccountRepository;

        public ExpertManager(
            IRepository<LhzxExpertAccount, long> expertAccountRepository,
            UserStore store,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<LhzxExpertAccount>> logger) : base(
            store,
            null,
            null,
            null,
            null,
            null,
            errors,
            services,
            logger)
        {
            _expertAccountRepository = expertAccountRepository;
        }

        public async Task<LhzxExpertAccount> FindByUserName(string userName)
        {
            return await _expertAccountRepository.FirstOrDefaultAsync(e => e.UserName == userName);
        }

        public override async Task<IdentityResult> CreateAsync(LhzxExpertAccount user)
        {
            Check.NotNull(user, nameof(user));

            user.CreationTime = DateTime.Now;
            user.IsDeleted = false;

            await _expertAccountRepository.InsertAsync(user);

            return IdentityResult.Success;
        }
    }
}
