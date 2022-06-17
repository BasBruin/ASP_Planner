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

        /// <summary>
        /// Je geeft hier een sql query mee en hij voert hem voor je uit.
        /// </summary>
        /// <param name="query">Geef hier de query die je wilt uitvoeren</param>
        /// <returns>Geeft een datareader terug die de data afleeest</returns>
        public SqlDataReader loadSQL(string query)
        {
            try
            {
                SqlConnection databaseConnection = new(connString);
                SqlCommand cmd = new(query, databaseConnection);
                cmd.CommandTimeout = 60;
                SqlDataReader reader;
                databaseConnection.Open();
                reader = cmd.ExecuteReader();
                return (reader);
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

