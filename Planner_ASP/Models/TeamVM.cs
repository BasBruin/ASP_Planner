using BusnLogicBW;

namespace Planner_ASP.Models
{
    public class TeamVM
    {
        public readonly int ID;
        public readonly string Naam;
        public readonly string Beschrijving;
        public readonly string Plaatje;

        public TeamVM(int id, string naam, string beschrijving)
        {
            this.ID = id;
            this.Naam = naam;
            this.Beschrijving = beschrijving;
        }

        public TeamVM(string naam, string beschrijving, string plaatje)
        {
            this.Naam = naam;
            this.Beschrijving = beschrijving;
            this.Plaatje = plaatje;
        }

        public TeamVM(Team t)
        {
            this.ID = t.ID;
            this.Naam = t.Naam;
            this.Beschrijving = t.Beschrijving;
            this.Plaatje = t.Plaatje;
        }

        public TeamVM()
        {

        }
    }
}
