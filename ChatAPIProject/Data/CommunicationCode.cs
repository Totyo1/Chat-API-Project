using ChatAPIProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Data
{
    public class CommunicationCode
    {
        private readonly string connString;

        public CommunicationCode()
        {
            this.connString = ConfigurationManager.AppSettings["myDbConnection"];
        }

        public List<Communication> All(int userId)
        {
            List<Communication> list = new List<Communication>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_com_all", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@usr_id_1", userId);

                using (var reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Communication communication = new Communication
                            {
                                Id = int.Parse(reader["communication_id"].ToString()),
                                FirstUserId = int.Parse(reader["user_first_id"].ToString()),
                                SecondUserId = int.Parse(reader["user_second_id"].ToString())
                            };

                            list.Add(communication);
                        }
                    }
                }

                conn.Close();
            }

            return list;
        }

        public void CreateCommunication(int firstUserId, int secondUserId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_com_cre", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@usr_id_1", firstUserId);
                cmd.Parameters.AddWithValue("@usr_id_2", secondUserId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public Communication GetCommunicationByUsers(int firstUserId, int secondUserId)
        {
            Communication communication = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_com_ser", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@usr_id_1", firstUserId);
                cmd.Parameters.AddWithValue("@usr_id_2", secondUserId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            communication = new Communication
                            {
                                Id = int.Parse(reader["communication_id"].ToString()),
                                FirstUserId = int.Parse(reader["user_first_id"].ToString()),
                                SecondUserId = int.Parse(reader["user_second_id"].ToString())
                            };
                        }
                    }
                }

                conn.Close();
            }

            return communication;
        }
    }
}