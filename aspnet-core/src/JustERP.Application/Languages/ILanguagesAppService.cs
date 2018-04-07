using System.Threading.Tasks;
using Abp.Application.Services;
using JustERP.Languages.Dto;

namespace JustERP.Languages
{
    public interface ILanguagesAppService : IAsyncCrudAppService<LanguageDto, int, GetLanguagesInput, CreateLanguageDto, LanguageDto>
    {
        Task SetDefaultLanguage(ChangeLanguageInput changeLanguageInput);

        Task<LanguageDto> GetDefaultLanguageOrNull(int? tenantId);
    }
}