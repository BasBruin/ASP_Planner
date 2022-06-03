using BusnLogicBW;
using DalMSSQL;
using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;

namespace Planner_ASP.Controllers
{
    public class RegisterTeamController : Controller
    {
        private GebruikerContainer gc = null;
        private TeamContainer tc = null;
        private readonly IConfiguration _configuration;

        public RegisterTeamController(IConfiguration ic)
        {
            _configuration = ic;
            tc = new (new TeamMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
            gc = new (new GebruikerMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Gebruiker> gebruikers = new(gc.GetAll());
            RegisterTeamViewModel vm = new(gebruikers);
            
            if (HttpContext.Session.GetString("ID") != null)
            {
                return View(vm);
            }
            return Content("Je moet eerst inloggen");
        }
        [HttpPost]
        public IActionResult Index(RegisterTeamViewModel registervm)
        {
            if (!tc.UsernameExists(registervm.Naam))
            {
                Team t = new(registervm.Naam, registervm.Beschrijving, registervm.Plaatje);
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
