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
    }
}