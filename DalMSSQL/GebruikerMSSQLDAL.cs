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
            connection = new SqlConnection(connString);
        }

        public bool UsernameExists(string Username)
        {
            reader = SQL.loadSQL("SELECT UserName FROM Gebruiker");
            reader.Read();
            if (reader.GetString("UserName") != Username)
            {
                return false;
            }
            return true;
        }

        public int Create(GebruikerDTO dto, string wachtwoord)
        {
            try
            {
                connection.Open();
                SqlCommand cmd;
                string sql = "INSERT INTO Gebruiker(Naam, UserName, WachtWoord, GameNaam, Email, Rank1s, Rank2s, Rank3s)  Values(" +
                    "@Naam," +
                    "@UserName," +
                    "@Wachtwoord," +
                    "@GameNaam," +
                    "@Email," +
                    "@Rank1s," +
                    "@Rank2s," +
                    "@Rank3s); SELECT SCOPE_IDENTITY();";

                cmd = new SqlCommand(sql, connection);

                string hash = BCrypt.Net.BCrypt.EnhancedHashPassword(wachtwoord, 13);

                cmd.Parameters.AddWithValue("@Naam", dto.Naam);
                cmd.Parameters.AddWithValue("@UserName", dto.PlannerNaam);
                cmd.Parameters.AddWithValue("@WachtWoord", hash);
                cmd.Parameters.AddWithValue("@GameNaam", dto.GameNaam);
                cmd.Parameters.AddWithValue("@Email", dto.Email);
                cmd.Parameters.AddWithValue("@Rank1s", string.IsNullOrEmpty(dto.Rank1s) ? (object)DBNull.Value : dto.Rank1s);
                cmd.Parameters.AddWithValue("@Rank2s", string.IsNullOrEmpty(dto.Rank2s) ? (object)DBNull.Value : dto.Rank2s);
                cmd.Parameters.AddWithValue("@Rank3s", string.IsNullOrEmpty(dto.Rank3s) ? (object)DBNull.Value : dto.Rank3s);

                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch(Exception ex)
            {
                return -1;
            }
        }

        public void Delete(GebruikerDTO gebruiker)
        {
            SQL.loadSQL("DELETE FROM Gebruiker WHERE ID = '" + gebruiker.ID + "'");
        }

        public GebruikerDTO FindByID(int ID)
        {
            try
            {
                reader = SQL.loadSQL("Select * FROM Gebruiker WHERE ID = '" + ID + "'");
                reader.Read();
                return new GebruikerDTO(reader.GetString("Naam"), reader.GetString("GameNaam"), reader.GetString("UserName"),
                    reader.GetString("Email"),
                    reader.IsDBNull("Rank1s") ? null : reader.GetString("Rank1s"),
                    reader.IsDBNull("Rank2s") ? null : reader.GetString("Rank2s"),
                    reader.IsDBNull("Rank3s") ? null : reader.GetString("Rank3s"),
                    reader.GetInt32("ID"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void Update(GebruikerDTO gebruiker)
        {
            reader = SQL.loadSQL("UPDATE Gebruiker SET Naam = '" + gebruiker.Naam + "', UserName = '" + gebruiker.PlannerNaam + "', GameNaam = '" + gebruiker.GameNaam + "', Email = '" + gebruiker.Email + "', Rank1s = '" + gebruiker.Rank1s + "', Rank2s = '" + gebruiker.Rank2s + "', Rank3s = '" + gebruiker.Rank3s + "', WHERE ID = '" + gebruiker.ID + "'");
        }

        // Dit is voor mensen toevoegen aan team.
        public SqlDataReader GetAllGebruikers()
        {
            reader = SQL.loadSQL("SELECT * FROM Gebruiker");
            return (reader);
        }

        public List<GebruikerDTO> GetAll()
        {
            List<GebruikerDTO> lijst = new();
            try
            {
                reader = SQL.loadSQL("SELECT * FROM Gebruiker");
                while (reader.Read())
                {
                    lijst.Add(new GebruikerDTO(reader.GetString("Naam"), reader.GetString("GameNaam"), reader.GetString("UserName"), reader.GetString("Email"), reader.GetString("Rank1s"), reader.GetString("Rank2s"), reader.GetString("Rank3s"), reader.GetInt32("ID")));
                }
                return lijst;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }


        /// <summary>
        /// Checkt bij inlog of de gebruikersnaam en wachtwoord kloppen.
        /// </summary>
        /// <param name="gebruikersnaam"></param>
        /// <param name="wachtwoord"></param>
        /// <returns>GebruikerDTO</returns>
        public GebruikerDTO FindByUsernameAndPassword(string? gebruikersnaam, string? wachtwoord)
        {
            try
            {
                connection.Open();
                SqlCommand command;
                string sql = "SELECT * FROM Gebruiker WHERE UserName = @gebruikersnaam";

                command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@gebruikersnaam", gebruikersnaam);
                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    if (BCrypt.Net.BCrypt.EnhancedVerify(wachtwoord, reader.GetString("WachtWoord")))
                    {
                        int id = reader.GetInt32("ID");
                        connection.Close();
                        return FindByID(id);
                    }
                }
                connection.Close();
                return null;
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (IOException ex)
            {
                throw new TemporaryExceptionDAL("Temporary error with connection");
            }
            catch (SqlException ex)
            {
                throw new TemporaryExceptionDAL("No connection with server");
            }
            catch (Exception ex)
            {
                throw new PermanentExceptionDAL("Error Please Check our twitter for more updates.");
            }
        }
    }
}