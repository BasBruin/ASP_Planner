using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using InterfaceLib;
using System.Data;

namespace DalMSSQL
{
    public class TeamMSSQLDAL : ITeamContainer
    {
        private readonly string connString;
        DatabaseUtility SQL = null;
        SqlDataReader reader;

        public TeamMSSQLDAL(string cs)
        {
            connString = cs;
            SQL = new DatabaseUtility(connString);
        }

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

        public List<TeamDTO> GetAll()
        {
            List<TeamDTO> lijst = new();
            reader = SQL.loadSQL("SELECT * FROM Team");
            while (reader.Read())
            {
                lijst.Add(new TeamDTO(reader.GetInt32("ID"), reader.GetString("Naam"), reader.GetString("Beschrijving")));
            }
            return lijst;
        }

        public List<TeamDTO> GetMyTeams(int ID)
        {
            List<TeamDTO> lijst = new List<TeamDTO>();
            DataTable dt = new();
            string Sql = "SELECT t.* " +
            "FROM Team t " +
            "JOIN GebruikerTeam gt ON gt.TeamID = t.ID " +
            "WHERE gt.GebruikerID =  '" + ID + "'";
            SqlDataAdapter da = new(Sql, connString);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int TeamID = Convert.ToInt32(dr["ID"].ToString());
                lijst.Add(new TeamDTO(TeamID, dr["Naam"].ToString(), dr["Beschrijving"].ToString()));
            }
            return lijst;
        }

        public void Update(TeamDTO team)
        {
            SQL.loadSQL("UPDATE Team SET Naam = '" + team.Naam + "', Beschrijving = '" + team.Beschrijving + "', Plaatje = '" + team.Plaatje + "' WHERE ID = '" + team.ID + "'");
        }
    }
}
