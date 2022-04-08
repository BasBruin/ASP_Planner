using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using InterfaceLib;

namespace DalMSSQL
{
    public class TeamMSSQLDAL : ITeamContainer
    {
        SQL_Connection SQL = new();
        SqlDataReader reader;

        public int Create(TeamDTO team)
        {
            SQL.loadSQL("INSERT INTO Team(Naam, Beschrijving, Plaatje) VALUES('" + team.Naam + "', '" + team.Beschrijving + "', '" + team.Plaatje + "')");
            reader = SQL.loadSQL("SELECT ID, Naam FROM Team WHERE Naam = '" + team.Naam + "'");
            reader.Read();
            return reader.GetInt32(0);
        }
        public void Delete(TeamDTO team)
        {
            SQL.loadSQL("DELETE FROM Team WHERE ID = '" + team.ID + "'");
        }

        public TeamDTO FindByID(int ID)
        {
            reader = SQL.loadSQL("SELECT ID, Naam, Beschrijving FROM Team WHERE ID = '" + ID + "'");
            TeamDTO dTO = new(ID, reader.GetString(1), reader.GetString(2));
            return dTO;
        }

        public void Update(TeamDTO team)
        {
            throw new NotImplementedException();
        }
    }
}
