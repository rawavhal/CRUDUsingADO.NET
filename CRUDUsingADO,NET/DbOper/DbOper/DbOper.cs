using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;


namespace DbOper
{
    public class DbOpers
    {
        SqlConnection SqlConn;
        
		private string _ConnString { get; set; }
        public DbOpers(string ConnSting)
        {
            _ConnString = ConnSting;
        }

        public List<User> GetAllUsers()
        {
            List<User> UserList = new List<User>();

            using (SqlConn= new SqlConnection(_ConnString))
            {
                SqlConn.Open();

                SqlCommand SqlCmd = new SqlCommand("[dbo].[SP_GetAllUsers]", SqlConn);
                SqlCmd.CommandType = CommandType.StoredProcedure;

                var reader = SqlCmd.ExecuteReader();

                while (reader.Read())
                {

                    User user = new User()
                    {
                        UserId = Convert.ToInt32(reader["UserId"].ToString()),
                        UserName = reader["UserName"].ToString(),
                        Address = reader["Address"].ToString()
                    };

                    UserList.Add(user);

                }
            }

            SqlConn.Close();
            return UserList;
        }

        public User GetUser(int UserId)
        {
            User user = new User();

            using (SqlConn = new SqlConnection(_ConnString))
            {
                SqlConn.Open();

                SqlCommand SqlCmd = new SqlCommand("[dbo].[SP_GetUser]", SqlConn);
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@UserId", UserId);
                SqlCmd.Parameters.Add(param);

                var reader = SqlCmd.ExecuteReader();

                while (reader.Read())
                {
                    user.UserId = Convert.ToInt32(reader["UserId"].ToString());
                    user.UserName = reader["UserName"].ToString();
                    user.Address = reader["Address"].ToString();
                }

            }

            SqlConn.Close();

            return user;
        }

        public int EditUser(User user, string QueryType = "")
        {
            int count = 0;

            using (SqlConn = new SqlConnection(_ConnString))
            {
                SqlConn.Open();

                SqlCommand SqlCmd;
                if (QueryType.ToUpper() == "CREATE")
                    SqlCmd = new SqlCommand("[dbo].[SP_CreateUser]", SqlConn);
                else
                    SqlCmd = new SqlCommand("[dbo].[SP_EditUser]", SqlConn);

                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@UserId", user.UserId);
                SqlCmd.Parameters.Add(param);

                param = new SqlParameter("@UserName", user.UserName);
                SqlCmd.Parameters.Add(param);

                param = new SqlParameter("@Address", user.Address);
                SqlCmd.Parameters.Add(param);

                count = SqlCmd.ExecuteNonQuery();

            }

            SqlConn.Close();

            return count;
        }

        public int DeleteUser(int UserId)
        {
            int count = 0;

            using (SqlConn = new SqlConnection(_ConnString))
            {
                SqlConn.Open();

                SqlCommand SqlCmd = new SqlCommand("[dbo].[SP_DeleteUser]", SqlConn);
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter param = new SqlParameter("@UserId", UserId);
                SqlCmd.Parameters.Add(param);

                count = SqlCmd.ExecuteNonQuery();

            }

            SqlConn.Close();
            return count;
        }

    }
}
