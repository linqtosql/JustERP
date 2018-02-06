using Abp.Application.Services.Dto;
using Microsoft.AspNetCore.Mvc;

namespace JustERP.MetronicTable.Dto
{
    public class MetronicPagedResultRequestDto : PagedResultRequestDto
    {
        [FromQuery(Name = "pagination[page]")]
        public int Page { get; set; } = 1;
        [FromQuery(Name = "pagination[perpage]")]
        public int Perpage { get; set; } = 10;
        [FromQuery(Name = "pagination[pages]")]
        public int? Pages { get; set; }
        public int Total { get; set; }
        [FromQuery(Name = "sort[sort]")]
        public string Sort { get; set; }
        [FromQuery(Name = "sort[field]")]
        public string Field { get; set; }

        public override int SkipCount => (Page - 1) * Perpage;

        public override int MaxResultCount => Perpage;
    }
}
