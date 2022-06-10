using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogicBW
{
    public class Rank
    {
        public readonly string Naam;

        public Rank(string rankName)
        {
            Naam = rankName;
        }
        public Rank(RankDTO dto)
        {
            Naam = dto.RankName;
        }

        public override string ToString()
        {
            return Naam;
        }
    }
}
