using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLib;

namespace BusnLogicBW
{
    public class Gebruiker
    {
        private readonly int ID;
        private readonly string Naam;
        private readonly string GameNaam;
        private readonly string PlannerNaam;
        private readonly string Email;
        private readonly string Rank1s;
        private readonly string Rank2s;
        private readonly string Rank3s;

        public Gebruiker(int ID, string naam, string gameNaam, string plannerNaam, string email,
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

        public override string ToString()
        {
            return GameNaam;
        }
    }
}
