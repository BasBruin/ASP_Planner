using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogicBW
{
    public class RankContainer
    {
        private readonly IRankContainer container;

        public RankContainer(IRankContainer container)
        {
            this.container = container;
        }

        public List<Rank> GetRanks()
        {
            List<RankDTO> dtos = container.GetRanks();
            List<Rank> ranks = new();
            foreach (RankDTO dto in dtos)
            {
                ranks.Add(new Rank(dto));
            }
            return ranks;
        }
    }
}
