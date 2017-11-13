using JustERP.Web.Core.User.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace JustERP.Web.Host.User.Controllers
{
    public class HomeController : JustERPControllerBase
    {
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
