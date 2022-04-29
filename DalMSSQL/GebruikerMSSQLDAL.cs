using System.Data;
using System.Data.SqlClient;
using InterfaceLib;
using Org.BouncyCastle.Crypto.Generators;

namespace DalMSSQL
{
    public class GebruikerMSSQLDAL : IGebruikerContainer
    {
        private readonly string connString;
        DatabaseUtility SQL = null;
        SqlDataReader reader;
        SqlConnection connection = null;

        public GebruikerMSSQLDAL(string cs)
        {
            connString = cs;
            SQL = new DatabaseUtility(connString);
            SqlConnection connection = new SqlConnection(connString);
        }

        public int Create(GebruikerDTO dto, string wachtwoord)
        {
            SqlCommand cmd;
            string sql = "INSERT INTO Gebruiker(Naam, UserName, WachtWoord, GameNaam, Email, Rank1s, Rank2s, Rank3s) output Gebruiker.ID Values(" +
                "@Naam," +
                "@UserName," +
                "@Wachtwoord," +
                "@GameNaam," +
                "@Email," +
                "@Rank1s," +
                "@Rank2s," +
                "@Rank3s)";

            cmd = new SqlCommand(sql, connection);

            string hash = BCrypt.Net.BCrypt.EnhancedHashPassword(wachtwoord, 13);

            cmd.Parameters.AddWithValue("@Naam", dto.Naam);
            cmd.Parameters.AddWithValue("@UserName", dto.PlannerNaam);
            cmd.Parameters.AddWithValue("@WachtWoord", hash);
            cmd.Parameters.AddWithValue("@GameNaam", dto.GameNaam);
            cmd.Parameters.AddWithValue("@Email", dto.Email);
            cmd.Parameters.AddWithValue("@Rank1s", dto.Rank1s);
            cmd.Parameters.AddWithValue("@Rank2s", dto.Rank2s);
            cmd.Parameters.AddWithValue("@Rank3s", dto.Rank3s);

            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public void Delete(GebruikerDTO gebruiker)
        {
            SQL.loadSQL("DELETE FROM Gebruiker WHERE ID = '" + gebruiker.ID + "'");
        }

        public GebruikerDTO FindByID(int ID)
        {
            reader = SQL.loadSQL("Select * FROM Gebruiker WHERE ID = '" + ID + "'");
            reader.Read();
            return new GebruikerDTO(reader.GetInt32("ID"), reader.GetString("Naam"), reader.GetString("GameNaam"), reader.GetString("UserName"), reader.GetString("Email"), reader.GetString("Rank1s"), reader.GetString("Rank2s"), reader.GetString("Rank3s"));
        }

        public void Update(GebruikerDTO gebruiker)
        {
            reader = SQL.loadSQL("UPDATE Gebruiker SET Naam = '" + gebruiker.Naam + "', UserName = '" + gebruiker.PlannerNaam + "', GameNaam = '" + gebruiker.GameNaam + "', Email = '" + gebruiker.Email + "', Rank1s = '" + gebruiker.Rank1s + "', Rank2s = '" + gebruiker.Rank2s + "', Rank3s = '" + gebruiker.Rank3s + "', WHERE ID = '" + gebruiker.ID +"'");
        }

        // Dit is voor mensen toevoegen aan team.
        public SqlDataReader GetAllGebruikers()
        {
            reader = SQL.loadSQL("SELECT * FROM Gebruiker");
            return (reader);
        }

        public List<GebruikerDTO> GetAll()
        {
            List<GebruikerDTO> lijst = new List<GebruikerDTO>();
            reader = SQL.loadSQL("SELECT * FROM Gebruiker");
            while (reader.Read())
            {
                lijst.Add(new GebruikerDTO(reader.GetInt32("ID"), reader.GetString("Naam"), reader.GetString("GameNaam"), reader.GetString("UserName"), reader.GetString("Email"), reader.GetString("Rank1s"), reader.GetString("Rank2s"), reader.GetString("Rank3s")));
            }
            return lijst;
        }

        public GebruikerDTO FindByUsernameAndPassword(string gebruikersnaam, string wachtwoord)
        {
            connection.Open();
            SqlCommand command;
            string sql = "SELECT id FROM Gebruiker WHERE UserName = @gebruikersnaam AND WachtWoord = @wachtwoord";

            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@gebruikersnaam", gebruikersnaam);
            command.Parameters.AddWithValue("@wachtwoord", wachtwoord);
            object obj = command.ExecuteScalar();
            if (obj == null)
            {
                connection.Close();
                return null;
            }
            else
            {
                connection.Close();
                string idstring = obj.ToString();
                int id = Convert.ToInt32(idstring);
                return FindByID(id);
            }
        }
    }
}