using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Localization;
using JustERP.Authorization;
using JustERP.Languages.Dto;

namespace JustERP.Languages
{
    [AbpAuthorize(PermissionNames.Pages_Languages)]
    public class LanguagesAppService : JustERPAppServiceBase, ILanguagesAppService
    {
        private IApplicationLanguageManager _appLanguageManager;
        private IRepository<ApplicationLanguage> _languageRepository;

        public LanguagesAppService(IApplicationLanguageManager appLanguageManager,
            IRepository<ApplicationLanguage> languageRepository)
        {
            _appLanguageManager = appLanguageManager;
            _languageRepository = languageRepository;
        }

        public async Task<LanguageDto> Get(EntityDto<int> input)
        {
            return ObjectMapper.Map<LanguageDto>(await _languageRepository.GetAsync(input.Id));
        }

        public async Task<PagedResultDto<LanguageDto>> GetAll(GetLanguagesInput input)
        {
            var languages = await _appLanguageManager.GetLanguagesAsync(AbpSession.TenantId);
            return new PagedResultDto<LanguageDto>(languages.Count,
                ObjectMapper.Map<IReadOnlyList<LanguageDto>>(languages));
        }

        public async Task<LanguageDto> Create(CreateLanguageDto input)
        {
            var language = ObjectMapper.Map<ApplicationLanguage>(input);
            await _appLanguageManager.AddAsync(language);
            return ObjectMapper.Map<LanguageDto>(language);
        }

        public async Task<LanguageDto> Update(LanguageDto input)
        {
            var language = ObjectMapper.Map<ApplicationLanguage>(input);
            await _appLanguageManager.UpdateAsync(AbpSession.TenantId, language);
            return ObjectMapper.Map<LanguageDto>(language);
        }

        public async Task Delete(EntityDto<int> input)
        {
            var language = await _languageRepository.GetAsync(input.Id);
            await _appLanguageManager.RemoveAsync(AbpSession.TenantId, language.Name);
        }
    }
}