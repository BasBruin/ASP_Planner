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
            throw new NotImplementedException();
        }

        public void Update(GebruikerDTO gebruiker)
        {
            throw new NotImplementedException();
        }

        // Dit is voor mensen toevoegen aan team.
        public SqlDataReader GetAllGebruikers()
        {
            string query = "SELECT * FROM Gebruiker WHERE ID != 1";
            SqlConnection databaseConnection = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, databaseConnection);
            cmd.CommandTimeout = 60;
            SqlDataReader reader;
            databaseConnection.Open();
            reader = cmd.ExecuteReader();
            return (reader);
        }
    }
}