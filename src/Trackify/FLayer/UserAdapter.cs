using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Trackify
{
    public class UserAdapter
    {
        static SqlConnection con = new SqlConnection(Program.ConnectionString);
        public static User GetUserById(int Id)
        {
            User newuser = new User();
            string tmpId = Id.ToString();
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Select * From Users Where Id = "+ tmpId, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                return null;
            }
            newuser.Id = Convert.ToInt32(rd["Id"].ToString());   
            newuser.Country = rd["Country"].ToString();
            newuser.City = rd["City"].ToString();
            newuser.DateOfBirth = new Date(rd["DateOfBirth"].ToString());
            newuser.RealName = rd["RealName"].ToString();
            newuser.RealSurname = rd["RealSurname"].ToString();
            newuser.UserName = rd["UserName"].ToString();
            newuser.FollowerCount = Convert.ToInt32(rd["FollowerCount"].ToString());
            newuser.PassHash = rd["PasswordHash"].ToString();
            return newuser;
        }
        public static User GetUserByUserName(string UserName)
        {
            User newuser = new User();
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Select * From Users Where UserName = " + UserName, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                con.Close();
                return null;
            }
            newuser.Id = Convert.ToInt32(rd["Id"].ToString());
            newuser.Country = rd["Country"].ToString();
            newuser.City = rd["City"].ToString();
            newuser.DateOfBirth = new Date(rd["DateOfBirth"].ToString());
            newuser.RealName = rd["RealName"].ToString();
            newuser.RealSurname = rd["RealSurname"].ToString();
            newuser.UserName = rd["UserName"].ToString();
            newuser.FollowerCount = Convert.ToInt32(rd["FollowerCount"].ToString());
            newuser.PassHash = rd["PasswordHash"].ToString();
            con.Close();
            return newuser;
        }
        public static void RemoveUser(User user)
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Delete From Users Where Id = " + user.Id.ToString(), con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void AddUser(User user)
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            string query = "Insert Into Users(Country, City, DateOfBirth, RealName, RealSurname," +
                " UserName, FollowerCount, PasswordHash) values(" +
                "\'" + user.Country + "\' , " +
                "\'" + user.City + "\' , " +
                "\'" + Date.FromDate(user.DateOfBirth) + "\' , " +
                "\'" + user.RealName + "\' , " +
                "\'" + user.RealSurname + "\' , " +
                "\'" + user.UserName + "\' , " +
                user.FollowerCount + " , " +
                "\'" + user.PassHash + "\');";
            var cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void Updateuser(User user)
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            string query = "UPDATE Users SET " +
                "Country = \'"+ user.Country + "\' , "+
                "City = \'" + user.City + "\' , " +
                "RealName = \'" + user.RealName + "\' , " +
                "RealSurname = \'" + user.RealSurname + "\' , " +
                "UserName = \'" + user.UserName + "\' , " +
                "FollowerCount = " + user.FollowerCount + " , " +
                "PasswordHash = \'" + user.PassHash + "\' , " +
                "DateOfBirth = \'" + Date.FromDate(user.DateOfBirth) + "\' " +
                "WHERE Id = "+user.Id;
            var cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static List<User> ListUsers()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            List<User> UserList = new List<User>();
            con.Open();
            var cmd = new SqlCommand("Select * From Users ", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                User newuser = new User();
                newuser.Id = Convert.ToInt32(rd["Id"].ToString());
                newuser.Country = rd["Country"].ToString();
                newuser.City = rd["City"].ToString();
                newuser.DateOfBirth = new Date(rd["DateOfBirth"].ToString());
                newuser.RealName = rd["RealName"].ToString();
                newuser.RealSurname = rd["RealSurname"].ToString();
                newuser.UserName = rd["UserName"].ToString();
                newuser.FollowerCount = Convert.ToInt32(rd["FollowerCount"].ToString());
                newuser.PassHash = rd["PasswordHash"].ToString();
                UserList.Add(newuser);
            }
            con.Close();
            return UserList;
        }
    }
}