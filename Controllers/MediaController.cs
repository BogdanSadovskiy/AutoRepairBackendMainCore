using Microsoft.AspNetCore.Mvc;

namespace AutoRepairMainCore.Controllers
{
    public class MediaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
