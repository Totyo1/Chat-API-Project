using ChatAPIProject.Models.InputModels.FriendRequest;
using Models.ServiceModels.FriendRequest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ChatAPIProject.Data
{
    public class FriendRequestCode
    {
        private readonly string connectionString;
        public FriendRequestCode()
        {
            this.connectionString = ConfigurationManager.AppSettings["myDbConnection"];
        }

        public void SendFriendRequest(FriendRequestInputModel model)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("tdb_frr_ins", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@snd_id", model.SenderId);
                cmd.Parameters.AddWithValue("@rsv_id", model.ReceiverId);
                cmd.Parameters.AddWithValue("@status", model.Status);

                cmd.ExecuteNonQuery();
                conn.Close();
            };
        }

        public List<FriendServiceModel> GetFriends(int userId, string status)
        {
            var list = new List<FriendServiceModel>();
            using(SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("tdb_frr_fr", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@usr_id", userId);
                cmd.Parameters.AddWithValue("@req_status", status);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            FriendServiceModel friend = new FriendServiceModel
                            {
                                FriendId = int.Parse(reader["user_id"].ToString())
                            };

                            list.Add(friend);
                        }
                    }
                }

                conn.Close();
            }

            return list;
        }

        public void DeleteUserRequests(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("tdb_frr_dlt", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<RequestServiceModel> GetFriendRequests(int userId, string status)
        {
            List<RequestServiceModel> list = new List<RequestServiceModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("tdb_frr_all", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@usr_id", userId);
                cmd.Parameters.AddWithValue("@req_status", status);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            RequestServiceModel friend = new RequestServiceModel
                            {
                                FriendId = int.Parse(reader["user_id"].ToString())
                            };

                            list.Add(friend);
                        }
                    }
                }

                conn.Close();
            }

            return list;
        }

        public void AcceptRequest(int userId, int receiverId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("tdb_frr_acc", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@usr_id", userId);
                cmd.Parameters.AddWithValue("@usr_rec_id", receiverId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void RejectRequest(int userId, int receiverId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("tdb_frr_rej", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@usr_id", userId);
                cmd.Parameters.AddWithValue("@usr_rec_id", receiverId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
