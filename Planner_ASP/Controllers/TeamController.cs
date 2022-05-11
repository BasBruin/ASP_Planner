using BusnLogicBW;
using DalMSSQL;
using InterfaceLib;
using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;

namespace Planner_ASP.Controllers
{
    public class TeamController : Controller
    {
        private TeamContainer tc = null;

        private readonly IConfiguration _configuration;

        public TeamController(IConfiguration ic)
        {
            _configuration = ic;
            tc = new(new TeamMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        public IActionResult Index() // localhost/team
        {
            List<Team> teams = new(tc.GetAll());
            List<TeamViewModel> vms = new();
            foreach (Team t in teams)
            {
                vms.Add(new TeamViewModel(t));
            }
            return View(vms);
        }
    }
}
