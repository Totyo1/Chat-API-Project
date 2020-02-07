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

        public User GetUserByUsernameAndPassword(string username, string password)
        {
            User user = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_usr_ext", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@usr_nme", username);
                cmd.Parameters.AddWithValue("@pwd", password);
                using (var reader = cmd.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        user = new User
                        {
                            Id = Convert.ToInt32(reader["user_id"]),
                            Username = reader["username"].ToString(),
                            Password = reader["password"].ToString()
                        };
                    }
                }
                conn.Close();
            }

            return user;
        }
    }
}