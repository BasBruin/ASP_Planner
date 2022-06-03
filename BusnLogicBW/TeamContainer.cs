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

        public bool UsernameExists(string Username)
        {
            return container.UsernameExists(Username);
        }

        public List<Team> GetAll()
        {
            List<TeamDTO> dtos = container.GetAll();
            List<Team> teams = new List<Team>();
            foreach (TeamDTO dto in dtos)
            {
                teams.Add(new Team(dto));
            }
            return teams;
        }
        public List<Team> GetMyTeams(int ID)
        {
            List<TeamDTO> dtos = container.GetMyTeams(ID);
            List<Team> teams = new List<Team>();
            foreach (TeamDTO dto in dtos)
            {
                teams.Add(new Team(dto));
            }
            return teams;
        }
    }
}
