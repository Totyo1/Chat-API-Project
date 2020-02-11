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
        
        public bool SendMessage(MessageServiceModel model)
        {
            throw new NotImplementedException();
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

        public void DeleteFriendMeesages(int commId)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_msg_com_id_dlt", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@id", commId);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}