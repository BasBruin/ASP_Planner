using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceLib
{
    public interface ITeamContainer
    {
        public TeamDTO FindByID(int ID);
        public int Create(TeamDTO team);
        public void Update(TeamDTO team);
        public void Delete(TeamDTO team);
        public List<TeamDTO> GetAll();
        public List<TeamDTO> GetMyTeams(int ID);
        public bool UsernameExists(string Username);
    }
}
