using ChatAPIProject.Models.DataModels;

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
        public void CreateUser(UserDataModel user)
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

        public UserDataModel GetUserByUsernameAndPassword(string username, string password)
        {
            UserDataModel user = null;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("tdb_usr_ext", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                cmd.Parameters.AddWithValue("@usr_nme", username);
                cmd.Parameters.AddWithValue("@pwd", password);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            user = new UserDataModel
                            {
                                Id = int.Parse(reader["user_id"].ToString()), //(int)reader["user_id"],  //reader.GetInt32((int)reader.GetOrdinal("user_id")), //Convert.ToInt32(reader["user_id"]), //reader.GetInt32(1), //reader.GetString((int)reader.GetOrdinal("user_id")),  //(int)(reader["user_id"]),
                                Username = reader["username"].ToString(),
                                Password = reader["password"].ToString()
                            };
                        }
                    }
                }
                conn.Close();
            }

            return user;
        }
    }
}