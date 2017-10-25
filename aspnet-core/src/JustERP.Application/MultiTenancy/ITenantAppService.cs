using Abp.Application.Services;
using Abp.Application.Services.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;
using JustERP.MultiTenancy.Dto;

namespace JustERP.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedResultRequestDto, CreateTenantDto, TenantDto>, IMetronicTableAppService<TenantDto, MetronicPagedResultRequestDto>
    {
    }
}
