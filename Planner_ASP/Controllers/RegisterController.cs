using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;
using System.ComponentModel.DataAnnotations;
using DalMSSQL;
using System.Data.SqlClient;
using System.Data;
using InterfaceLib;
using BusnLogicBW;

namespace Planner_ASP.Controllers
{
    public class RegisterController : Controller
    {
        private GebruikerContainer gc;
        private RankContainer rc;

        private readonly IConfiguration _configuration;

        public RegisterController(IConfiguration ic)
        {
            _configuration = ic;
            rc = new RankContainer(new RankMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
            gc = new GebruikerContainer(new GebruikerMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                List<Rank> ranks = rc.GetRanks();
                RegisterViewModel vm = new(ranks);
                return View(vm);
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
        public IActionResult Index(RegisterViewModel registervm)
        {
            try
            {
                if (IsValidEmailAddress(registervm.Email))
                {
                    if (registervm.Wachtwoord == registervm.WachtwoordHerhalen)
                    {
                        if (!gc.UsernameExists(registervm.PlannerNaam))
                        {
                            Gebruiker g = new(registervm.Naam, registervm.GameNaam, registervm.PlannerNaam, registervm.Email, registervm.Rank1s, registervm.Rank2s, registervm.Rank3s);
                            int id = gc.Create(g, registervm.Wachtwoord);
                            HttpContext.Session.SetInt32("ID", id);
                            HttpContext.Session.SetString("Naam", g.Naam);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewData["ErrorGebNaam"] = "Gebruikersnaam bestaat al!";
                        }
                    }
                    else
                    {
                        ViewData["ErrorWW"] = "Wachtwoord en wachtwoord herhalen is niet hetzelfde!";
                    }
                }
                else
                {
                    ViewData["ErrorEmail"] = "Ongeldige email";
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

        /// <summary>
        /// Checkt of het meegegeven email het patroon van een echt email volgt.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>true als het op een emailadres lijkt of false</returns>
        public bool IsValidEmailAddress(string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
                return true;
            else
                return false;
        }
    }
}
