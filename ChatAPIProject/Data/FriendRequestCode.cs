using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatAPIProject.Models.InputModels.FriendRequest;

namespace ChatAPIProject.Data
{
    public class FriendRequestCode
    {
        private readonly string connString;
        public FriendRequestCode()
        {
            this.connString = ConfigurationManager.AppSettings["myDbConnection"];
        }

        public void SendFriendRequest(FriendRequestInputModel model)
        {
            using (SqlConnection conn = new SqlConnection(connString))
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
    }
}
