using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DalMSSQL
{
    public class DatabaseUtility
    {
        private readonly string connString;

        public DatabaseUtility(string cs)
        {
            connString = cs;
        }

        public DatabaseUtility()
        {

        }

        public SqlDataReader loadSQL(string query)
        {
            SqlConnection databaseConnection = new(connString);
            SqlCommand cmd = new(query, databaseConnection);
            cmd.CommandTimeout = 60;
            SqlDataReader reader;
            databaseConnection.Open();
            reader = cmd.ExecuteReader();
            return (reader);
        }
    }
}
