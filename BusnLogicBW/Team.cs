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
        private int ID;
        private string Naam;
        private string Beschrijving;
        private string Plaatje;
        
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

        public override string? ToString()
        {
            return Naam;
        }
    }
}
