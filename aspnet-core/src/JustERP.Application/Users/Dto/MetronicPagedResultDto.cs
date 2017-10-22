using System.Collections.Generic;

namespace JustERP.Users.Dto
{
    public class MetronicPagedResultDto<T>
    {
        public IReadOnlyList<T> Data { get; set; }

        public MetronicPagedResultRequestDto Meta { get; set; }
    }
}
