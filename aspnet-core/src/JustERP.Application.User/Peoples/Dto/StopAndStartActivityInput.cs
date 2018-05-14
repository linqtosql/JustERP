using System;
using Abp.Application.Services.Dto;

namespace JustERP.Application.User.Peoples.Dto
{
    public class StopAndStartActivityInput : EntityDto<long>
    {
        public DateTime EndTime { get; set; } = DateTime.Now;
    }
}
