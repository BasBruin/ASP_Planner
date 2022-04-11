using BusnLogicBW;
using DalMSSQL;
using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;

namespace Planner_ASP.Controllers
{
    public class GebruikerController : Controller
    {
        GebruikerContainer gc = new GebruikerContainer(new GebruikerMSSQLDAL());

        public IActionResult Index() // localhost/verkoper
        {
            List<Gebruiker> gebruikers = new(gc.GetAll());
            List<GebruikerVM> vms = new();
            foreach(Gebruiker g in gebruikers)
            {
                vms.Add(new GebruikerVM(g.ID, g.Naam, g.GameNaam, g.PlannerNaam, g.Email, g.Rank1s, g.Rank2s, g.Rank3s));
            }
            return View(vms);
        }

        public IActionResult Detail(int id)
        {
            Gebruiker g = gc.FindByID(id);
            GebruikerVM vm = new(g.ID, g.Naam, g.GameNaam, g.PlannerNaam, g.Email, g.Rank1s, g.Rank2s, g.Rank3s);
            return View(vm);
        }
    }
}
