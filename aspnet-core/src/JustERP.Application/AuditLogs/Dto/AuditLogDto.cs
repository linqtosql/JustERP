using System;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.AutoMapper;

namespace JustERP.AuditLogs.Dto
{
    [AutoMapFrom(typeof(AuditLog))]
    public class AuditLogDto : EntityDto<int>
    {
        public string UserName { get; set; }
        public DateTime ExecutionTime { get; set; }
        public string ServiceName { get; set; }
        public string MethodName { get; set; }
        public string ClientIpAddress { get; set; }
        public string ClientName { get; set; }
    }
}
