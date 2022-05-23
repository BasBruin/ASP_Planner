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
        public readonly string RankName;

        public Rank(string rankName)
        {
            RankName = rankName;
        }
        public Rank(RankDTO dto)
        {
            RankName = dto.RankName;
        }

        public override string ToString()
        {
            return RankName;
        }
    }
}
