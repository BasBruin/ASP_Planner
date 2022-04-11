using BusnLogicBW;
using DalMSSQL;
using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;

namespace Planner_ASP.Controllers
{
    public class LoginController : Controller
    {
        GebruikerContainer gc = new(new GebruikerMSSQLDAL());

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel loginVM)
        {
            if (ModelState.IsValid)
            {
                Gebruiker g = gc.FindByUsernameAndPassword(loginVM.Gebruikersnaam, loginVM.Wachtwoord);
                if (g == null)
                {
                    return Content("Gebruikersnaam of wachtwoord ongeldig");
                }
                else
                {
                    return Content($"Hallo {g}");
                }
            }
            return Content("Niet alle velden zijn gevuld");
        }
    }
}
