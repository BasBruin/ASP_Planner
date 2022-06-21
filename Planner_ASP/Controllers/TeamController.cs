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
        private ReviewContainer rc;

        private readonly IConfiguration _configuration;

        public TeamController(IConfiguration ic)
        {
            _configuration = ic;
            tc = new(new TeamMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
            rc = new(new ReviewMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        public IActionResult Index()
        {
            try
            {
                List<Team> alleteams = new(tc.GetAll());
                List<Team> mijnteams = new(tc.GetMyTeams((int)HttpContext.Session.GetInt32("ID")));
                List<TeamViewModel> vms = new();
                foreach (Team t in mijnteams)
                {
                    vms.Add(new TeamViewModel(t));
                }
                if (HttpContext.Session.GetString("ID") != null)
                {
                    return View(vms);
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
        [HttpGet]
        public IActionResult Detail(int ID)
        {
            try
            {
                if (HttpContext.Session.GetString("Naam") != null)
                {
                    List<Review> reviews = rc.GetTeamReviews(ID);
                    Team t = tc.FindByID(ID);
                    TeamViewModel vm = new(t, reviews);
                    HttpContext.Session.SetInt32("TeamID", ID);
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
        [HttpGet]
        public IActionResult Review()
        {
            ReviewViewModel reviewViewModel = new();
            return View(reviewViewModel);
        }
        [HttpPost]
        public IActionResult Review(ReviewViewModel reviewViewModel)
        {
            Review review = new(reviewViewModel.Teamreview, (int)HttpContext.Session.GetInt32("TeamID"));
            rc.Create(review);
            return RedirectToAction("Index", "Team");
        }
    }
}
