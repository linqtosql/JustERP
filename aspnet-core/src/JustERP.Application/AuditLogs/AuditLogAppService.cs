using Abp.Auditing;
using Abp.Authorization;
using Abp.Domain.Repositories;
using JustERP.AuditLogs.Dto;
using JustERP.Authorization;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;

namespace JustERP.AuditLogs
{
    [AbpAuthorize(PermissionNames.Pages_AuditLogs)]
    public class AuditLogAppService : BaseMetronicTableAppService<AuditLog, AuditLogDto, long, MetronicPagedResultRequestDto>, IAuditLogAppService
    {
        public AuditLogAppService(IRepository<AuditLog, long> repository) : base(repository)
        {
        }
    }
}
