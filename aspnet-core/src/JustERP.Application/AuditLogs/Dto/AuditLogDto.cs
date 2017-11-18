using System;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;

namespace JustERP.AuditLogs.Dto
{
    [AutoMapFrom(typeof(AuditLog))]
    public class AuditLogDto : EntityDto<long>
    {
        public DateTime ExecutionTime { get; set; }
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public string ClientName { get; set; }
        public int ExecutionDuration { get; set; }
        public string BrowserInfo { get; set; }
        public string Parameters { get; set; }
    }
}
