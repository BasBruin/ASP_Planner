using BusnLogicBW;

namespace Planner_ASP.Models
{
    public class RegisterTeamViewModel
    {
        public string Naam { get; set; }
        public string Beschrijving { get; set; }
        public string? Plaatje { get; set; }
        public List<Gebruiker> gebruikers { get; set; }
        public int Teamspeler2 { get; set; }
        public int Teamspeler3 { get; set; }

        public RegisterTeamViewModel()
        {
        }

        public RegisterTeamViewModel(string naam, string beschrijving, string? plaatje)
        {
            Naam = naam;
            Beschrijving = beschrijving;
            Plaatje = plaatje;
        }
        public RegisterTeamViewModel(List<Gebruiker> gebruikers)
        {
            this.gebruikers = gebruikers;
        }
    }
}
