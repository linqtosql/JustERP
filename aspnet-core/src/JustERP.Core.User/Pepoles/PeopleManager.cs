using System;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace JustERP.Core.User.Pepoles
{
    public class PeopleManager : UserManager<MtPeople>, IDomainService
    {
        private IRepository<MtPeople, long> _peopleRepository;
        public PeopleManager(
            IRepository<MtPeople, long> peopleRepository,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
            IServiceProvider services, ILogger<UserManager<MtPeople>> logger) :
            base(null, null, null, null, null, keyNormalizer, errors, services, logger)
        {
            _peopleRepository = peopleRepository;
        }

        public async Task<MtPeople> FindByOpenId(string openId)
        {
            return await _peopleRepository.FirstOrDefaultAsync(e => e.Openid == openId);
        }
    }
}
