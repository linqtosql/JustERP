using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;

namespace JustERP.OrganizationUnits.Dto
{
    public class CreateOrganizationUnitDto : IShouldNormalize
    {
        [Required]
        [StringLength(128)]
        public string DisplayName { get; set; }

        public void Normalize()
        {

        }
    }
}
