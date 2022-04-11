using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DalMSSQL
{
    public class SQL_Connection
    {
        string connString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi487790;Persist Security Info=True;User ID=dbi487790;Password=Welkom12";
        
        public SqlDataReader loadSQL(string query)
        {
            SqlConnection databaseConnection = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, databaseConnection);
            cmd.CommandTimeout = 60;
            SqlDataReader reader;
            databaseConnection.Open();
            reader = cmd.ExecuteReader();
            return (reader);
        }

        public static SqlConnection SqlConnectie { get; private set; }

        public static void SetConnection(string connectieString)
        {
            SqlConnectie = new SqlConnection(connectieString);
        }

        public static void Open()
        {
            if (SqlConnectie.State != ConnectionState.Open)
            {
                SqlConnectie.Open();
            }
        }

        public static void Close()
        {
            if (SqlConnectie.State != ConnectionState.Closed)
            {
                SqlConnectie.Close();
            }
        }

    }
}
