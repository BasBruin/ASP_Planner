using BusnLogicBW;
using InterfaceLib;

namespace Planner_ASP.Models
{
    public class GebruikerViewModel
    {
        public int? ID;
        public string ?Naam;
        public string ?GameNaam;
        public string ?PlannerNaam;
        public string ?Email;
        public string ?Rank1s;
        public string ?Rank2s;
        public string ?Rank3s;

        public GebruikerViewModel(string naam, string? gameNaam, string? plannerNaam, string? email, string? rank1s, string? rank2s, string? rank3s, int? Id = null)
        {
            ID = Id;
            Naam = naam;
            GameNaam = gameNaam;
            PlannerNaam = plannerNaam;
            Email = email;
            Rank1s = rank1s;
            Rank2s = rank2s;
            Rank3s = rank3s;
        }

        public GebruikerViewModel(Gebruiker g)
        {
            this.ID = g.ID;
            this.Naam = g.Naam;
            this.GameNaam = g.GameNaam;
            this.PlannerNaam = g.PlannerNaam;
            this.Email = g.Email;
            this.Rank1s = g.Rank1s;
            this.Rank2s = g.Rank2s;
            this.Rank3s = g.Rank3s;
        }

        public GebruikerViewModel()
        {

        }
    }
}
