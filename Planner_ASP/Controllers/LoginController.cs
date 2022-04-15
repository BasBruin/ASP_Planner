using BusnLogicBW;
using DalMSSQL;
using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;

namespace Planner_ASP.Controllers
{
    public class LoginController : Controller
    {
        private GebruikerContainer gc = null;

        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration ic)
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
                    HttpContext.Session.SetString("ID", g.ID.ToString());
                    HttpContext.Session.SetString("Naam", g.Naam);
                    return RedirectToAction("Index", "Home");
                }
            }
            return Content("Niet alle velden zijn gevuld");
        }
    }
}
