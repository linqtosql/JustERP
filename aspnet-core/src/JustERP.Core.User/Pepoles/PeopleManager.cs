using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JustERP.Core.User.Pepoles
{
    public class PeopleManager : UserManager<MtPeople>, IDomainService
    {
        private IRepository<MtPeople, long> _peopleRepository;
        public PeopleManager(
            IRepository<MtPeople, long> peopleRepository,
            IUserStore<MtPeople> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<MtPeople> passwordHasher,
            IEnumerable<IUserValidator<MtPeople>> userValidators,
            IEnumerable<IPasswordValidator<MtPeople>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<MtPeople>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task<MtPeople> FindByOpenId(string openId)
        {
            return await _peopleRepository.FirstOrDefaultAsync(e => e.Openid == openId);
        }
    }
}
