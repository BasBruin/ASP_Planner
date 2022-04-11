using System.Data;
using System.Data.SqlClient;
using InterfaceLib;

namespace DalMSSQL
{
    public class GebruikerMSSQLDAL : IGebruikerContainer
    {
        string connString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi487790;Persist Security Info=True;User ID=dbi487790;Password=Welkom12";
        SQL_Connection SQL = new();
        SqlDataReader reader;

        public int Create(GebruikerDTO gebruiker)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        // Dit is voor mensen toevoegen aan team.
        public SqlDataReader GetAllGebruikers()
        {
            string query = "SELECT * FROM Gebruiker";
            SqlConnection databaseConnection = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, databaseConnection);
            cmd.CommandTimeout = 60;
            SqlDataReader reader;
            databaseConnection.Open();
            reader = cmd.ExecuteReader();
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
    }
}