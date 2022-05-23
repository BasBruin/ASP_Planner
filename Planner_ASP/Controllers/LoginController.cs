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
                try 
                {
                    Gebruiker g = gc.FindByUsernameAndPassword(loginVM.Gebruikersnaam, loginVM.Wachtwoord);
                    if (g == null)
                    {
                        ViewData["Error"] = "Inloggegevens niet correct";
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("ID", (int)g.ID);
                        HttpContext.Session.SetString("Naam", g.Naam);
                        HttpContext.Session.SetString("PlannerNaam", g.PlannerNaam);
                        return RedirectToAction("Index", "Home");
                    }
                }
                catch(TemporaryExceptionDAL ex)
                {
                    Console.WriteLine(ex.Message + "Please try again.");
                    return Redirect("/tijderr.html");
                }
                catch (PermanentExceptionDAL ex)
                {
                    return Redirect("https://twitter.com/bassie00001");
                }
            }
            return View();
        }

        public IActionResult Uitloggen()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
