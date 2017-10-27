using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Organizations;
using Abp.Runtime.Validation;

namespace JustERP.OrganizationUnits.Dto
{
    [AutoMapTo(typeof(OrganizationUnit))]
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
