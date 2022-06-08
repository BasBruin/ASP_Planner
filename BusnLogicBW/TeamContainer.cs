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

        public void VoegSpelerAanTeam(int GebruikerID, int TeamID, bool IsBeheerder)
        {
            container.VoegSpelerAanTeam(GebruikerID, TeamID, IsBeheerder);
        }


        public int Create(Team t)
        {
            TeamDTO dto = t.GetDTO();
            return container.Create(dto);
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
