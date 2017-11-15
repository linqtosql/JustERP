using JustERP.MetronicTable.Dto;

namespace JustERP.Users.Dto
{
    public class GetUsersDto : MetronicPagedResultRequestDto
    {
        public string Search { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}
