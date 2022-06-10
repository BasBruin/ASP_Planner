using BusnLogicBW;
using DalMSSQL;
using InterfaceLib;
using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;

namespace Planner_ASP.Controllers
{
    public class TeamController : Controller
    {
        private TeamContainer tc;

        private readonly IConfiguration _configuration;

        public TeamController(IConfiguration ic)
        {
            _configuration = ic;
            tc = new(new TeamMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        public IActionResult Index() // localhost/team
        {
            List<Team> alleteams = new(tc.GetAll());
            List<Team> mijnteams = new(tc.GetMyTeams((int)HttpContext.Session.GetInt32("ID")));
            List<TeamViewModel> vms = new();
            foreach (Team t in mijnteams)
            {
                vms.Add(new TeamViewModel(t));
            }
            if(HttpContext.Session.GetString("ID") != null)
            {
                return View(vms);
            }
            return Content("Je moet eerst inloggen");
        }
    }
}
