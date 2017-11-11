using JustERP.MetronicTable.Dto;

namespace JustERP.Users.Dto
{
    public class GetUsersRequestDto : MetronicPagedResultRequestDto
    {
        public string Search { get; set; }
        public long OrganizationUnitId { get; set; }
    }
}
