using Abp.Auditing;
using Abp.Domain.Repositories;
using JustERP.AuditLogs.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;

namespace JustERP.AuditLogs
{
    public class AuditLogAppService : BaseMetronicTableAppService<AuditLog, AuditLogDto, long, MetronicPagedResultRequestDto>, IAuditLogAppService
    {
        public AuditLogAppService(IRepository<AuditLog, long> repository) : base(repository)
        {
        }
    }
}
