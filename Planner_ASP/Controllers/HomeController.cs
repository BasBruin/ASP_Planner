using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;
using System.Diagnostics;

namespace Planner_ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string naam = HttpContext.Session.GetString("Naam");
            if(naam == null)
            {
                return View();
            }
            return Content($"Hallo {naam}");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}