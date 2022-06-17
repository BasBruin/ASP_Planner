using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMSSQL
{
    public class RankMSSQLDAL : IRankContainer
    {
        private readonly string connString;
        DatabaseUtility SQL;
        SqlConnection connection;

        public RankMSSQLDAL(string cs)
        {
            connString = cs;
            SQL = new DatabaseUtility(connString);
            connection = new SqlConnection(connString);
        }

        /// <summary>
        /// Hier krijg je alle Ranks in de database
        /// </summary>
        /// <returns>Geeft een lijst met alle ranks terug</returns>
        public List<RankDTO> GetRanks()
        {
            try
            {
                connection.Open();
                List<RankDTO> rankDTOs = new List<RankDTO>();

                string query = "SELECT * FROM Ranks";
                SqlCommand command = new SqlCommand(query, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    rankDTOs.Add(new RankDTO(reader.GetString("Rank")));
                }
                connection.Close();
                return rankDTOs;
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
