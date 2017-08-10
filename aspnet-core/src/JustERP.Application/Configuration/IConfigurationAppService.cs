using System.Threading.Tasks;
using JustERP.Configuration.Dto;

namespace JustERP.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}