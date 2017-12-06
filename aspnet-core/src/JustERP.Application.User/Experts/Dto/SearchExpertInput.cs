using System.ComponentModel.DataAnnotations;

namespace JustERP.Application.User.Experts.Dto
{
    public class SearchExpertInput
    {
        [Required(ErrorMessage = "请输入你要搜索的专家名字或者领域")]
        public string Keyword { get; set; }
    }
}
