using Abp.Application.Services.Dto;

namespace JustERP.MetronicTable.Dto
{
    public class MetronicPagedResultRequestDto : PagedResultRequestDto
    {
        public int Page { get; set; } = 1;
        public int Pages { get; set; }
        public int Perpage { get; set; } = 10;
        public int Total { get; set; }
        public string Sort { get; set; }
        public string Field { get; set; }

        public override int SkipCount => (Page - 1) * Perpage;

        public override int MaxResultCount => Perpage;
    }
}
