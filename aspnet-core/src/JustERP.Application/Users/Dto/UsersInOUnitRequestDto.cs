using JustERP.MetronicTable.Dto;

namespace JustERP.Users.Dto
{
    public class UsersInOUnitRequestDto : MetronicPagedResultRequestDto
    {
        public long OrganizationUnitId { get; set; }
    }
}
