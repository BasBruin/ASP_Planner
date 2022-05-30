using BusnLogicBW;

namespace Planner_ASP.Models
{
    public class TeamViewModel
    {
        public readonly int ID;
        public readonly string Naam;
        public readonly string Beschrijving;
        public readonly string? Plaatje;
        public readonly List<ReviewViewModel>? viewModel;
        

        public TeamViewModel(int iD, string naam, string beschrijving, string? plaatje,
            List<ReviewViewModel>? viewModel) : this(iD, naam, beschrijving)
        {
            Plaatje = plaatje;
            this.viewModel = viewModel;
        }

        public TeamViewModel(int id, string naam, string beschrijving)
        {
            this.ID = id;
            this.Naam = naam;
            this.Beschrijving = beschrijving;
        }

        public TeamViewModel(string naam, string beschrijving, string plaatje)
        {
            this.Naam = naam;
            this.Beschrijving = beschrijving;
            this.Plaatje = plaatje;
        }

        public TeamViewModel(Team t)
        {
            this.ID = (int)t.ID;
            this.Naam = t.Naam;
            this.Beschrijving = t.Beschrijving;
            this.Plaatje = t.Plaatje;
        }

        public TeamViewModel()
        {

        }
    }
}
