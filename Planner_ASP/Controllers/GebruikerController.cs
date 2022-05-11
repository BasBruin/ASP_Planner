using BusnLogicBW;
using DalMSSQL;
using Microsoft.AspNetCore.Mvc;
using Planner_ASP.Models;

namespace Planner_ASP.Controllers
{
    public class GebruikerController : Controller
    {
        private GebruikerContainer gc = null;

        private readonly IConfiguration _configuration;

        public GebruikerController(IConfiguration ic)
        {
            _configuration = ic;
            gc = new GebruikerContainer(new GebruikerMSSQLDAL(_configuration["ConnectionStrings:Connstring"]));
        }

        public IActionResult Index() // localhost/gebruiker
        {
            List<Gebruiker> gebruikers = new(gc.GetAll());
            List<GebruikerViewModel> vms = new();
            foreach (Gebruiker g in gebruikers)
            {
                vms.Add(new GebruikerViewModel(g));
            }
            return View(vms);
        }

        public IActionResult Detail(int id)
        {
            Gebruiker g = gc.FindByID(id);
            GebruikerViewModel vm = new(g);
            return View(vm);
        }
    }
}
