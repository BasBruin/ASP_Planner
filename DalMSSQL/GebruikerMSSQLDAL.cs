﻿using System.Data;
using System.Data.SqlClient;
using InterfaceLib;
using Org.BouncyCastle.Crypto.Generators;

namespace DalMSSQL
{
    public class GebruikerMSSQLDAL : IGebruikerContainer
    {
        private readonly string connString;
        private readonly DatabaseUtility SQL;
        SqlDataReader? reader;
        private readonly SqlConnection connection;

        /// <summary>
        /// Hier maak je een GebruikerDAL.
        /// </summary>
        /// <param name="cs">Hier geef je de connectiestring mee</param>
        public GebruikerMSSQLDAL(string cs)
        {
            connString = cs;
            SQL = new DatabaseUtility(connString);
            connection = new SqlConnection(connString);
        }

        /// <summary>
        /// Kijkt of de gebruikersnaam die je meegeeft al in de database bestaat of niet.
        /// </summary>
        /// <param name="Username">Hier geef je een gebruikersnaam, waarbij je wilt checken of die bestaat</param>
        /// <returns>true als de gebruikersnaam al bestaat, false als dat niet is</returns>
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

        /// <summary>
        /// Maakt een gebruiker aan in de database
        /// </summary>
        /// <param name="dto">Hier geef je ene DTO die alle info van de gebruiker heeft</param>
        /// <param name="wachtwoord">Hier geef je het wachtwoord van het account mee</param>
        /// <returns>ID van de aangemaakte gebruiker</returns>
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
            catch (Exception ex)
            {
                throw new PermanentExceptionDAL("Error, Please Check our twitter for more updates.", ex);
            }
        }

        /// <summary>
        /// Verwijdert de meegegeven gebruiker in de database, en verwijdert hem van zijn teams
        /// </summary>
        /// <param name="gebruiker">Gebruiker die je wilt verwijderen</param>
        public void Delete(int id)
        {
            DeleteFromAllTeam(id);
            connection.Open();
            SqlCommand cmd;
            string sql = "DELETE FROM Gebruiker WHERE ID = @ID";
            cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.ExecuteNonQuery();
            
        }

        /// <summary>
        /// Verwijdert een gebruiker van al zijn teams
        /// </summary>
        /// <param name="gebruikerID">Geef hier de gebruikerID mee die je wilt verwijderen van zijn teams</param>
        private void DeleteFromAllTeam(int gebruikerID)
        {
            connection.Open();
            SqlCommand cmd;
            string sql = "DELETE FROM GebruikerTeam WHERE GebruikerID = @GebruikerID";
            cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@GebruikerID", gebruikerID);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Vind de gebruiker waarbij de meegegeven ID bij hoort.
        /// </summary>
        /// <param name="ID">De ID van de gebruiker die je wilt vinden</param>
        /// <returns>GebruikerDTO van de gebruikerID die je mee hebt gegeven</returns>
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
            catch (SqlException ex)
            {
                throw new PermanentExceptionDAL("Error Please Check our twitter for more updates.", ex);
            }
        }

        /// <summary>
        /// Update de info van de gebruiker in de database
        /// </summary>
        /// <param name="gebruiker">Hier geef je de gebruiker mee die je wilt updaten in de database.</param>
        public void Update(GebruikerDTO gebruiker)
        {
            try
            {
                connection.Open();
                SqlCommand cmd;
                string sql = "UPDATE Gebruiker SET Naam = @Naam, UserName = @UserName, GameNaam = @GameNaam," +
                    "Email = @Email, Rank1s = @Rank1s, Rank2s = @Rank2s, Rank3s = @Rank3s " +
                    "WHERE ID = @ID";


                cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@Naam", gebruiker.Naam);
                cmd.Parameters.AddWithValue("@UserName", gebruiker.PlannerNaam);
                cmd.Parameters.AddWithValue("@GameNaam", gebruiker.GameNaam);
                cmd.Parameters.AddWithValue("@Email", gebruiker.Email);
                cmd.Parameters.AddWithValue("@Rank1s", gebruiker.Rank1s);
                cmd.Parameters.AddWithValue("@Rank2s", gebruiker.Rank2s);
                cmd.Parameters.AddWithValue("@Rank3s", gebruiker.Rank3s);
                cmd.Parameters.AddWithValue("@ID", gebruiker.ID);
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (InvalidOperationException ex)
            {
                throw new TemporaryExceptionDAL("Temporary error with connection", ex);
            }
            catch (IOException ex)
            {
                throw new TemporaryExceptionDAL("Temporary error with connection", ex);
            }
            catch (SqlException ex)
            {
                throw new PermanentExceptionDAL("Error Please Check our twitter for more updates.", ex);
            }
        }

        /// <summary>
        /// Dit haalt alle gebruikers behalve de meegegeven speler op in de database en zet ze in een lijst
        /// </summary>
        /// <returns>Lijst van alle gebruikers in de database</returns>
        public List<GebruikerDTO>? GetAll(int id)
        {
            List<GebruikerDTO> lijst = new();
            try
            {
                reader = SQL.loadSQL("SELECT * FROM Gebruiker WHERE ID != '" + id + "'");
                while (reader.Read())
                {
                    lijst.Add(new GebruikerDTO(reader.GetString("Naam"), reader.GetString("GameNaam"), reader.GetString("UserName"), reader.GetString("Email"), reader.GetString("Rank1s"), reader.GetString("Rank2s"), reader.GetString("Rank3s"), reader.GetInt32("ID")));
                }
                return lijst;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Checkt bij inlog of de gebruikersnaam en wachtwoord kloppen.
        /// </summary>
        /// <param name="gebruikersnaam"></param>
        /// <param name="wachtwoord">Dit wordt maar even gebruikt.</param>
        /// <returns>null als gebruiker niet gevonden is, returnt GebruikerDTO</returns>
        public GebruikerDTO? FindByUsernameAndPassword(string? gebruikersnaam, string? wachtwoord)
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
                throw new TemporaryExceptionDAL("Temporary error with connection", ex);
            }
            catch (IOException ex)
            {
                throw new TemporaryExceptionDAL("Temporary error with connection", ex);
            }
            catch (SqlException ex)
            {
                throw new PermanentExceptionDAL("Error Please Check our twitter for more updates.", ex);
            }
        }
    }
}