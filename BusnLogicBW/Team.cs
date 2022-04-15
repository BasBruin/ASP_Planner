using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceLib;
using System.Data.SqlClient;
using System.Data;
using DalMSSQL;

namespace BusnLogicBW
{
    public class Team
    {
        public readonly int ID;
        public readonly string Naam;
        public readonly string Beschrijving;
        public readonly string Plaatje;
        
        public Team(int id, string naam, string beschrijving)
        {
            this.ID = id;
            this.Naam = naam;
            this.Beschrijving = beschrijving;
        }

        public Team(string naam, string beschrijving, string plaatje)
        {
            this.Naam = naam;
            this.Beschrijving = beschrijving;
            this.Plaatje = plaatje;
        }

        public Team(TeamDTO dto)
        {
            this.ID = dto.ID;
            this.Naam = dto.Naam;
            this.Beschrijving = dto.Beschrijving;
            this.Plaatje = dto.Plaatje;
        }

        public override string? ToString()
        {
            return Naam;
        }
    }
}
