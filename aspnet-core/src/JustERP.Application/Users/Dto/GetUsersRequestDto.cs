using JustERP.MetronicTable.Dto;

namespace JustERP.Users.Dto
{
    public class GetUsersRequestDto : MetronicPagedResultRequestDto
    {
        public string Keyword { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}
