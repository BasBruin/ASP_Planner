﻿using InterfaceLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalMSSQL
{
    public class ReviewMSSQLDAL : IReviewContainer
    {
        private readonly string connString;
        SqlConnection connection;

        public ReviewMSSQLDAL(string cs)
        {
            connString = cs;
            connection = new SqlConnection(connString);
        }


        /// <summary>
        /// Hier krijg je alle Reviews van een specifiek team
        /// </summary>
        /// <param name="ID">Dit is het ID van het team</param>
        /// <returns>Lijst van reviews over dat team</returns>
        public List<ReviewDTO> GetTeamReviews(int ID)
        {
            List<ReviewDTO> lijst = new List<ReviewDTO>();
            DataTable dt = new();
            string Sql = "SELECT rw.* " +
            "FROM Review rw " +
            "JOIN Team t ON t.ID = rw.TeamID " +
            "WHERE t.ID = '" + ID + "'";
            SqlDataAdapter da = new(Sql, connection);
            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                int RevID = Convert.ToInt32(dr["ID"]);
                int TeamID = Convert.ToInt32(dr["TeamID"]);
                lijst.Add(new ReviewDTO(dr["Review"].ToString(), TeamID, RevID));
            }
            return lijst;
        }

        /// <summary>
        /// Je maakt hier een review aan over een team
        /// </summary>
        /// <param name="review">Je geeft hier alle info van de review mee</param>
        public void Create(ReviewDTO review)
        {
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            SqlCommand command;
            string sql = "INSERT INTO Review(Review, TeamID) VALUES(" +
            "@Review," +
            "@TeamID)";

            command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Review", review.Teamreview);
            command.Parameters.AddWithValue("@TeamID", review.TeamID);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
