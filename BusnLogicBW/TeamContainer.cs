using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusnLogicBW
{
    public class TeamContainer
    {
        private readonly ITeamContainer container;

        public TeamContainer(ITeamContainer container)
        {
            this.container = container;
        }

        public List<Team> GetAll()
        {
            List<TeamDTO> dtos = container.GetAll();
            List<Team> gebruikers = new List<Team>();
            foreach (TeamDTO dto in dtos)
            {
                gebruikers.Add(new Team(dto));
            }
            return gebruikers;
        }
    }
}
