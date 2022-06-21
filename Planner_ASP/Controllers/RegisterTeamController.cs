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
            try
            {
                List<Gebruiker> gebruikers = gc.GetAll((int)HttpContext.Session.GetInt32("ID"));
                RegisterTeamViewModel vm = new(gebruikers);

                if (HttpContext.Session.GetInt32("ID") != null)
                {
                    return View(vm);
                }
                return RedirectToAction("Index", "Login");
            }
            catch (TemporaryExceptionDAL)
            {
                return RedirectToAction("Index", "TempError");
            }
            catch (PermanentExceptionDAL)
            {
                return Redirect("https://twitter.com/bassie00001");
            }
        }

        [HttpPost]
        public IActionResult Index(RegisterTeamViewModel registervm)
        {
            try
            {
                if (!tc.UsernameExists(registervm.Naam))
                {
                    Team t = new(registervm.Naam, registervm.Beschrijving, plaatje: registervm.Plaatje);
                    if (registervm.Teamspeler2 != registervm.Teamspeler3)
                    {
                        int teamid = tc.Create(t);
                        tc.VoegSpelerAanTeam(registervm.Teamspeler2, teamid, false);
                        tc.VoegSpelerAanTeam(registervm.Teamspeler3, teamid, false);
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
            catch (TemporaryExceptionDAL)
            {
                return RedirectToAction("Index", "TempError");
            }
            catch (PermanentExceptionDAL)
            {
                return Redirect("https://twitter.com/bassie00001");
            }
        }
    }
}
