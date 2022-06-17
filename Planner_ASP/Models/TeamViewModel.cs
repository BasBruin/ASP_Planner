using BusnLogicBW;

namespace Planner_ASP.Models
{
    public class TeamViewModel
    {
        public readonly int? ID;
        public readonly string? Naam;
        public readonly string? Beschrijving;
        public readonly string? Plaatje;
        public readonly List<Review>? reviews;
        

        public TeamViewModel(int id, string naam, string beschrijving)
        {
            this.ID = id;
            this.Naam = naam;
            this.Beschrijving = beschrijving;
        }

        public TeamViewModel(Team t, List<Review>? reviews = null)
        {
            this.ID = (int)t.ID;
            this.Naam = t.Naam;
            this.Beschrijving = t.Beschrijving;
            this.Plaatje = t.Plaatje;
            this.reviews = reviews;
        }

        public TeamViewModel()
        {

        }
    }
}
