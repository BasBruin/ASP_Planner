using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public class GebruikerDTO
    {
        public readonly int ID;
        public readonly string Naam;
        public readonly string GameNaam;
        public readonly string PlannerNaam;
        public readonly string Email;
        public readonly string Rank1s;
        public readonly string Rank2s;
        public readonly string Rank3s;

        public GebruikerDTO(int ID, string naam, string gameNaam, string plannerNaam, string email,
            string rank1s, string rank2s, string rank3s)
        {
            this.ID = ID;
            this.Naam = naam;
            this.GameNaam = gameNaam;
            this.PlannerNaam = plannerNaam;
            this.Email = email;
            this.Rank1s = rank1s;
            this.Rank2s = rank2s;
            this.Rank3s = rank3s;
        }
    }
}
