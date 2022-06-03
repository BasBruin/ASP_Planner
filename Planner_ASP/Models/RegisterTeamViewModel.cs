using BusnLogicBW;

namespace Planner_ASP.Models
{
    public class RegisterTeamViewModel
    {
        public readonly string Naam;
        public readonly string Beschrijving;
        public readonly string? Plaatje;
        public readonly List<Gebruiker> Gebruikers;
        public readonly Gebruiker Teamspeler2;
        public readonly Gebruiker Teamspeler3;

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
            this.Gebruikers = gebruikers;
        }
    }
}
