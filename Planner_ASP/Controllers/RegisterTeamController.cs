using BusnLogicBW;
using DalMSSQL;
using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;

namespace Planner_ASP.Controllers
{
    public class RegisterTeamController : Controller
    {
        private GebruikerContainer gc;
        private TeamContainer tc;
        private readonly IConfiguration _configuration;

        public RegisterTeamController(IConfiguration ic)
        {
            _configuration = ic;
            tc = new(new TeamMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
            gc = new(new GebruikerMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Gebruiker> gebruikers = gc.GetAll();
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
                if (registervm.Teamspeler2 != registervm.Teamspeler3)
                {
                    int teamid = tc.Create(t);
                    tc.VoegSpelerAanTeam(registervm.Teamspeler2.ID.Value, teamid, false);
                    tc.VoegSpelerAanTeam(registervm.Teamspeler3.ID.Value, teamid, false);
                    tc.VoegSpelerAanTeam(HttpContext.Session.GetInt32("ID").Value, teamid, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewData["ZelfdeTeamMate"] = "Je mag niet 2x dezelfde teamgenoot kiezen";
                }
            }
            else
            {
                ViewData["ZelfdeNaam"] = "TeamNaam bestaat al!";
            }
            return View();
        }
    }
}
