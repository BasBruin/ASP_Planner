using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace DalMSSQL
{
    public class SQL_Connection
    {
        private readonly string connString;

        public SQL_Connection(string cs)
        {
            connString = cs;
        }

        public SQL_Connection()
        {

        }

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
    }
}
