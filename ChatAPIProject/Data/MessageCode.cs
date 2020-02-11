using ChatAPIProject.Models.DataModels;
using ChatAPIProject.Models.ServiceModels.Message;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ChatAPIProject.Data
{
    public class MessageCode
    {
        private readonly string connString;

        public MessageCode()
        {
            this.connString = ConfigurationManager.AppSettings["myDbConnection"];
        }
        
        public void SendMessage(int communicationId,string content,int userId,int receiverId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_msg_cre", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@com_id", communicationId);
                cmd.Parameters.AddWithValue("@con_txt", content);
                cmd.Parameters.AddWithValue("@usr_id", userId);
                cmd.Parameters.AddWithValue("@rec_id", receiverId);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public void DeleteUsersMessages(int id)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("user_author_id", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@id",id);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public List<MessageServiceModel> GetMessagesByCommunicationId(int communicationId)
        {
            List<MessageServiceModel> list = new List<MessageServiceModel>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_msg_bid", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@com_id", communicationId);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            MessageServiceModel communication = new MessageServiceModel
                            {
                                Id = int.Parse(reader["msg_id"].ToString()),
                                CommunicationId = int.Parse(reader["communication_id"].ToString()),
                                Date = reader["date"].ToString(),
                                Content = reader["content_text"].ToString(),
                                AuthorId = int.Parse(reader["user_author_id"].ToString()),
                                ReceiverId = int.Parse(reader["receiver_id"].ToString())
                            };

                            list.Add(communication);
                        }
                    }
                }

                conn.Close();
            }

            return list;
        }
    }
}