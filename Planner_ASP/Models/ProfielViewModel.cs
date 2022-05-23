using BusnLogicBW;
using InterfaceLib;

namespace Planner_ASP.Models
{
    public class ProfielViewModel
    {
        public int? ID { get; set; }
        public string? Naam { get; set; }
        public string? GameNaam { get; set; }
        public string? PlannerNaam { get; set; }
        public string? Email { get; set; }
        public string? Rank1s { get; set; }
        public string? Rank2s { get; set; }
        public string? Rank3s { get; set; }
        public string? Plaatje { get; set; }
        public Gebruiker gebruiker { get; set; }
        public List<Rank> ranks { get; set; }

        public ProfielViewModel()
        {
        }

        public ProfielViewModel(Gebruiker? g, List<Rank>? Ranks, int? iD = null, string? naam = null, string? gameNaam = null, string? plannerNaam = null, string? email = null,
            string? rank1s = null, string? rank2s = null, string? rank3s = null, string? plaatje = null)
        {
            ranks = Ranks;
            gebruiker = g;
            ID = iD;
            Naam = naam;
            GameNaam = gameNaam;
            PlannerNaam = plannerNaam;
            Email = email;
            Rank1s = rank1s;
            Rank2s = rank2s;
            Rank3s = rank3s;
            Plaatje = plaatje;
        }
    }
}
