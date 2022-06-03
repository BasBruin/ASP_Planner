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

        /// <summary>
        /// Hiermee maak je een team aan in de database
        /// </summary>
        /// <param name="team">Geef hier een team mee die je wilt aanmaken</param>
        /// <returns>Geeft ID van het team terug die je hebt aangemaakt</returns>
        public int Create(TeamDTO team)
        {
            SQL.loadSQL("INSERT INTO Team(Naam, Beschrijving, Plaatje) VALUES('" + team.Naam + "', '" + team.Beschrijving + "', '" + team.Plaatje + "')");
            reader = SQL.loadSQL("SELECT ID, Naam FROM Team WHERE Naam = '" + team.Naam + "'");
            reader.Read();
            return reader.GetInt32(0);
        }

        /// <summary>
        /// Je kan hiermee een team verwijderen
        /// </summary>
        /// <param name="team">Je moet hier een team meegeven die je wilt verwijderen</param>
        public void Delete(TeamDTO team)
        {
            SQL.loadSQL("DELETE FROM Team WHERE ID = '" + team.ID + "'");
        }

        /// <summary>
        /// Hier kan je een team vinden op hun ID
        /// </summary>
        /// <param name="ID">Geef hier het teamID</param>
        /// <returns>Een teamDTO</returns>
        public TeamDTO FindByID(int ID)
        {
            reader = SQL.loadSQL("SELECT ID, Naam, Beschrijving FROM Team WHERE ID = '" + ID + "'");
            TeamDTO dTO = new(ID, reader.GetString(1), reader.GetString(2));
            return dTO;
        }

        /// <summary>
        /// Je krijgt hier alle teams in de database
        /// </summary>
        /// <returns>Geeft een lijst van alle teams</returns>
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

        /// <summary>
        /// Hier krijg je alle teams van de specifieke gebruiker
        /// </summary>
        /// <param name="ID">Hier geef je de gebruikerID mee waarvan je de teams wil</param>
        /// <returns>Hier krijg je alle teams van de specifieke gebruiker terug</returns>
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

        /// <summary>
        /// Hier update je het gespecifieerde team
        /// </summary>
        /// <param name="team">Hier geef je een team mee die je wilt updaten</param>
        public void Update(TeamDTO team)
        {
            SQL.loadSQL("UPDATE Team SET Naam = '" + team.Naam + "', Beschrijving = '" + team.Beschrijving + "', Plaatje = '" + team.Plaatje + "' WHERE ID = '" + team.ID + "'");
        }

        public bool UsernameExists(string Username)
        {
            reader = SQL.loadSQL("SELECT Naam FROM Team");
            reader.Read();
            if (reader.GetString("Naam") != Username)
            {
                return false;
            }
            return true;
        }
    }
}
