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
        public readonly int? ID;
        public readonly string Naam;
        public readonly string GameNaam;
        public readonly string PlannerNaam;
        public readonly string Email;
        public readonly string Rank1s;
        public readonly string Rank2s;
        public readonly string Rank3s;

        public Gebruiker(string naam, string gameNaam, string plannerNaam, string email,
            string rank1s, string rank2s, string rank3s, int? ID = null)
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

        public Gebruiker(GebruikerDTO dto)
        {
            this.ID = dto.ID;
            this.Naam = dto.Naam;
            this.GameNaam = dto.GameNaam;
            this.PlannerNaam = dto.PlannerNaam;
            this.Email = dto.Email;
            this.Rank1s = dto.Rank1s;
            this.Rank2s = dto.Rank2s;
            this.Rank3s = dto.Rank3s;
        }

        public override string ToString()
        {
            return GameNaam;
        }

        
        public GebruikerDTO GetDTO()
        {
            GebruikerDTO dto = new(Naam, GameNaam, PlannerNaam, Email, Rank1s, Rank2s, Rank3s, ID);
            return dto;
        }
    }
}
