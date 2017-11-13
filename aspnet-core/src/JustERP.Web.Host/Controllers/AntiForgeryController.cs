using JustERP.Web.Core.Admin.Controllers;
using Microsoft.AspNetCore.Antiforgery;

namespace JustERP.Web.Host.Controllers
{
    public class AntiForgeryController : JustERPControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}