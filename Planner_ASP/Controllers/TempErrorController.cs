using Microsoft.AspNetCore.Mvc;

namespace Planner_ASP.Controllers
{
    public class TempErrorController : Controller
    {
        public IActionResult Index(string error)
        {
            return View();
        }
    }
}
