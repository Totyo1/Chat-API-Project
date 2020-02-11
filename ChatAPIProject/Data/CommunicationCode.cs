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

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Communication communication = new Communication
                            {
                                Id = int.Parse(reader["com_id"].ToString()),
                                FirstUserId = int.Parse(reader["first_user_id"].ToString()),
                                SecondUserId = int.Parse(reader["second_user_id"].ToString())
                            };

                            list.Add(communication);
                        }
                    }
                }

                conn.Close();
            }

            return list;
        }

        public int DeleteFriend(int myId, int friendId)
        {
            int id = 0;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_com_fr_dlt", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@id_1", myId);
                cmd.Parameters.AddWithValue("@id_2", friendId);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        id = int.Parse(dr["id"].ToString());
                    }
                }
                conn.Close();
            }
            return id;
        }

        public void DeletUeserCommunications(int id)
        {
            int result;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_conn_dlt", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@id", id);
                
                cmd.ExecuteNonQuery();
                conn.Close();
            }
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

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            communication = new Communication
                            {
                                Id = int.Parse(reader["communication_id"].ToString()),
                                FirstUserId = int.Parse(reader["first_user_id"].ToString()),
                                SecondUserId = int.Parse(reader["second_user_id"].ToString())
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