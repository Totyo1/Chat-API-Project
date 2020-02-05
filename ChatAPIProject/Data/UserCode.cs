using ChatAPIProject.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ChatAPIProject.Data
{
    public class UserCode
    {
        private readonly string connString;
        public UserCode()
        {
            this.connString = ConfigurationManager.AppSettings["myDbConnection"];
        }
        public void CreateUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_usr_ins", conn);

                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();

                cmd.Parameters.AddWithValue("@usr_nme", user.Username);
                cmd.Parameters.AddWithValue("@pwd", user.Password);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}