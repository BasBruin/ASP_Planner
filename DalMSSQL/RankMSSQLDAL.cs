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
        /// <returns></returns>
        public List<RankDTO> GetRanks()
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
    }
}
