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
        DatabaseUtility DB = new();
        private GebruikerContainer gc;

        private readonly IConfiguration _configuration;

        public RegisterController(IConfiguration ic)
        {
            _configuration = ic;
            gc = new GebruikerContainer(new GebruikerMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RegisterViewModel registervm)
        {
            if (IsValidEmailAddress(registervm.Email))
            {
                if (registervm.Wachtwoord == registervm.WachtwoordHerhalen)
                {
                    if (!gc.UsernameExists(registervm.PlannerNaam))
                    {
                        Gebruiker g = new(registervm.Naam, registervm.GameNaam, registervm.PlannerNaam, registervm.Email, registervm.Rank1s, registervm.Rank2s, registervm.Rank3s);
                        int id = gc.Create(g, registervm.Wachtwoord);
                        HttpContext.Session.SetString("ID", id.ToString());
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

        /// <summary>
        /// Checkt of het meegegeven email het patroon van een echt email volgt.
        /// </summary>
        /// <param name="email"></param>
        /// <returns>bool</returns>
        public bool IsValidEmailAddress(string email)
        {
            if (new EmailAddressAttribute().IsValid(email))
                return true;
            else
                return false;
        }
    }
}
