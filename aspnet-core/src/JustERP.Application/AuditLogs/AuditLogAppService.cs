using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Auditing;
using Abp.Domain.Repositories;
using JustERP.AuditLogs.Dto;
using JustERP.MetronicTable.Dto;

namespace JustERP.AuditLogs
{
    public class AuditLogAppService : ApplicationService, IAuditLogAppService
    {
        private readonly IRepository<AuditLog, long> _auditLogRepository;
        public AuditLogAppService(IRepository<AuditLog, long> auditLogRepository)
        {
            _auditLogRepository = auditLogRepository;
        }
        public Task<MetronicPagedResultDto<AuditLogDto>> GetMetronicTable(MetronicPagedResultRequestDto input)
        {
            throw new NotImplementedException();
        }
    }
}
