namespace Planner_ASP.Models
{
    public class GebruikerVM
    {
        public int ID;
        public string Naam;
        public string ?GameNaam;
        public string ?PlannerNaam;
        public string ?Email;
        public string ?Rank1s;
        public string ?Rank2s;
        public string ?Rank3s;

        public GebruikerVM(int iD, string naam, string? gameNaam, string? plannerNaam, string? email, string? rank1s, string? rank2s, string? rank3s)
        {
            ID = iD;
            Naam = naam;
            GameNaam = gameNaam;
            PlannerNaam = plannerNaam;
            Email = email;
            Rank1s = rank1s;
            Rank2s = rank2s;
            Rank3s = rank3s;
        }

        public GebruikerVM()
        {

        }
    }
}
