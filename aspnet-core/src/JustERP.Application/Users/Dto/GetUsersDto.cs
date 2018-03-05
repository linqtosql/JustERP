using JustERP.MetronicTable.Dto;

namespace JustERP.Users.Dto
{
    public class GetUsersDto : MetronicPagedResultRequestDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
