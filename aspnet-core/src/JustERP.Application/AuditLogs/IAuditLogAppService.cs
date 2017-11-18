using JustERP.AuditLogs.Dto;
using JustERP.MetronicTable;
using JustERP.MetronicTable.Dto;

namespace JustERP.AuditLogs
{
    public interface IAuditLogAppService : IMetronicTableAppService<AuditLogDto, MetronicPagedResultRequestDto>
    {
        
    }
}