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
        DatabaseUtility SQL;
        SqlDataReader reader;
        private readonly SqlConnection connection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cs">Hier geef je de connectiestring mee</param>
        public TeamMSSQLDAL(string cs)
        {
            connString = cs;
            SQL = new DatabaseUtility(connString);
            connection = new SqlConnection(connString);
        }

        /// <summary>
        /// Hiermee maak je een team aan in de database
        /// </summary>
        /// <param name="team">Geef hier een team mee die je wilt aanmaken</param>
        /// <returns>Geeft ID van het team terug die je hebt aangemaakt</returns>
        public int Create(TeamDTO dto)
        {
            try
            {
                connection.Open();
                SqlCommand cmd;
                string sql = "INSERT INTO Team(Naam, Beschrijving, Plaatje)  Values(" +
                    "@Naam," +
                    "@Beschrijving," +
                    "@Plaatje);" +
                    "SELECT SCOPE_IDENTITY();";

                cmd = new SqlCommand(sql, connection);


                cmd.Parameters.AddWithValue("@Naam", dto.Naam);
                cmd.Parameters.AddWithValue("@Beschrijving", dto.Beschrijving);
                cmd.Parameters.AddWithValue("@Plaatje", string.IsNullOrEmpty(dto.Plaatje) ? (object)DBNull.Value : dto.Plaatje);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                throw new PermanentExceptionDAL("Error, Please Check our twitter for more updates.", ex);
            }
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
            TeamDTO dTO = new(ID, reader.GetString("Naam"), reader.GetString("Beschrijving"));
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

        /// <summary>
        /// Kijkt bij het aanmaken van een team of de teamnaam al bestaat
        /// </summary>
        /// <param name="Username">Geef hier de username waarbij je wilt checken of die al bestaat</param>
        /// <returns>Geeft true als hij al bestaat in de database, anders false</returns>
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

        /// <summary>
        /// Hier voeg je een meegegeven speler toe aan het meegegeven team en zegt of hij beheerder van het team is.
        /// </summary>
        /// <param name="GebruikerID">Geef hier de gebruiker die je wilt toeveogen</param>
        /// <param name="TeamID">Geef hier het team waaraan je de gebruiker wilt toevoegen</param>
        /// <param name="IsBeheerder">true als de gebruiker ook de beheerder is, anders false</param>
        /// <exception cref="PermanentExceptionDAL">Als de connectie niet werkt</exception>
        public void VoegGebruikerAanTeam(int GebruikerID, int TeamID, bool IsBeheerder)
        {
            try
            {
                connection.Open();
                SqlCommand cmd;
                string sql = "INSERT INTO GebruikerTeam(GebruikerID, TeamID, IsBeheerder)" +
                    "Values(@GebruikerID, @TeamID, @IsBeheerder)";
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@GebruikerID", GebruikerID);
                cmd.Parameters.AddWithValue("@TeamID", TeamID);
                cmd.Parameters.AddWithValue("@IsBeheerder", IsBeheerder);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw new PermanentExceptionDAL("Error, Please Check our twitter for more updates.", ex);
            }

        }
    }
}
