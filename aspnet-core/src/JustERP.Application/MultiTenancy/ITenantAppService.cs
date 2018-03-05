using Abp.Application.Services;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.MultiTenancy.Dto;

namespace JustERP.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, GetTenantInput, CreateTenantDto, TenantDto>, IMetronicTableAppService<TenantDto, GetTenantInput>
    {
    }
}
