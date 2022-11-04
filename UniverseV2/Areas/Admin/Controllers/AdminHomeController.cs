using Microsoft.AspNetCore.Mvc;

namespace UniverseV2.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
