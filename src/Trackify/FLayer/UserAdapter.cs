using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Trackify;
using Trackify.ELayer;
using Trackify.Log;

namespace Trackify.FLayer
{
    public class UserAdapter
    {
        private static readonly SqlConnection Con = new SqlConnection(Program.ConnectionString);
        public static User ParseUserJson(string userJson, int role)
        {
            dynamic jsonObject = JsonConvert.DeserializeObject(userJson);
            try
            {
                string birthDate = jsonObject.birthdate;
                string country = jsonObject.country;
                string displayName = jsonObject.display_name;
                string accountUrl = jsonObject.external_urls.spotify;
                string followerCount = jsonObject.followers.total;
                string userId = jsonObject.id;
                string profileImage = "";

                try
                {
                    profileImage = jsonObject.images[0].url;
                }
                catch (Exception)
                {

                }
                if (userId != null)
                {
                    User user = new User();
                    user.UserId = userId;
                    user.AccountUrl = accountUrl;
                    user.ImageUrl = profileImage;
                    user.DisplayName = displayName;
                    user.DateOfBirth = birthDate;
                    user.Country = country;
                    user.UserRole = role;
                    user.FollowerCount = Convert.ToInt32(followerCount);
                    if (GetUserBySpotifyUserId(userId) == null)
                    {
                        AddUser(user);
                    }
                    return user;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            return null;
        }
        public static User GetUserBySpotifyUserId(string userid)
        {
            User newuser = new User();
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            var cmd = new SqlCommand("Select * From Users Where UserId = \'" + userid + "\';", Con);
            var rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                Con.Close();
                return null;
            }
            newuser.Id = Convert.ToInt32(rd["Id"].ToString());
            newuser.UserId = rd["UserId"].ToString();
            newuser.DisplayName = rd["DisplayName"].ToString();
            newuser.AccountUrl = rd["SpotifyAccountUrl"].ToString();
            newuser.DateOfBirth = rd["DateOfBirth"].ToString();
            newuser.ImageUrl = rd["SpotifyImgUrl"].ToString();
            newuser.Country = rd["Country"].ToString();
            newuser.SpotifyAuthenticationToken = rd["SpotifyAuthenticationToken"].ToString();
            newuser.SpotifyRefreshToken = rd["SpotifyRefreshToken"].ToString();
            newuser.SpotifyToken = rd["SpotifyToken"].ToString();
            newuser.SpotifyTokenExpire = rd["SpotifyTokenExpire"].ToString();
            newuser.UserRole = Convert.ToInt32(rd["UserRole"].ToString());
            newuser.FollowerCount = Convert.ToInt32(rd["FollowerCount"].ToString());
            Con.Close();
            return newuser;
        }
        public static User GetUserById(int Id)
        {
            User newuser = new User();
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            var cmd = new SqlCommand("Select * From Users Where Id = \'" + Id + "\'", Con);
            var rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                Con.Close();
                return null;
            }
            newuser.Id = Convert.ToInt32(rd["Id"].ToString());
            newuser.UserId = rd["UserId"].ToString();
            newuser.DisplayName = rd["DisplayName"].ToString();
            newuser.AccountUrl = rd["SpotifyAccountUrl"].ToString();
            newuser.DateOfBirth = rd["DateOfBirth"].ToString();
            newuser.ImageUrl = rd["SpotifyImgUrl"].ToString();
            newuser.Country = rd["Country"].ToString();
            newuser.SpotifyAuthenticationToken = rd["SpotifyAuthenticationToken"].ToString();
            newuser.SpotifyRefreshToken = rd["SpotifyRefreshToken"].ToString();
            newuser.SpotifyToken = rd["SpotifyToken"].ToString();
            newuser.SpotifyTokenExpire = rd["SpotifyTokenExpire"].ToString();
            newuser.UserRole = Convert.ToInt32(rd["UserRole"].ToString());
            newuser.FollowerCount = Convert.ToInt32(rd["FollowerCount"].ToString());
            Con.Close();
            return newuser;
        }
        public static User GetUserByUserName(string displayName)
        {
            User newuser = new User();
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            var cmd = new SqlCommand("Select * From Users Where DisplayName = \'" + displayName + "\'" + "OR UserId = \'" + displayName + "\'", Con);
            var rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                Con.Close();
                return null;
            }
            newuser.Id = Convert.ToInt32(rd["Id"].ToString());
            newuser.UserId = rd["UserId"].ToString();
            newuser.DisplayName = rd["DisplayName"].ToString();
            newuser.AccountUrl = rd["SpotifyAccountUrl"].ToString();
            newuser.DateOfBirth = rd["DateOfBirth"].ToString();
            newuser.ImageUrl = rd["SpotifyImgUrl"].ToString();
            newuser.Country = rd["Country"].ToString();
            newuser.SpotifyAuthenticationToken = rd["SpotifyAuthenticationToken"].ToString();
            newuser.SpotifyRefreshToken = rd["SpotifyRefreshToken"].ToString();
            newuser.SpotifyToken = rd["SpotifyToken"].ToString();
            newuser.SpotifyTokenExpire = rd["SpotifyTokenExpire"].ToString();
            newuser.UserRole = Convert.ToInt32(rd["UserRole"].ToString());
            newuser.FollowerCount = Convert.ToInt32(rd["FollowerCount"].ToString());
            Con.Close();
            return newuser;
        }

        public static void RemoveUser(User user)
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            var cmd = new SqlCommand("Delete From Users Where UserId = '" + user.UserId + "\'", Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        public static void RemoveAllUsers()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();

            var cmd = new SqlCommand("Drop Table Users", Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public static void AddUser(User user)
        {
            using (var connection = new SqlConnection(Program.ConnectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        " INSERT INTO Users (UserId, SpotifyToken, UserRole, Country, SpotifyImgUrl, SpotifyAccountUrl, SpotifyAuthenticationToken, SpotifyTokenExpire," +
                        " SpotifyRefreshToken, DateOfBirth, DisplayName, FollowerCount)" +

                        "VALUES(@UserId, @SpotifyToken, @UserRole, @Country, @SpotifyImgUrl, @SpotifyAccountUrl, @SpotifyAuthenticationToken, " +
                        "@SpotifyTokenExpire, @SpotifyRefreshToken, @DateOfBirth, @DisplayName, @FollowerCount)";
                    command.Parameters.AddWithValue("@UserId", user.UserId ?? SqlString.Null);
                    command.Parameters.AddWithValue("@SpotifyToken", user.SpotifyToken ?? SqlString.Null);
                    command.Parameters.AddWithValue("@UserRole", user.UserRole);
                    command.Parameters.AddWithValue("@Country", user.Country ?? SqlString.Null);
                    command.Parameters.AddWithValue("@SpotifyImgUrl", user.ImageUrl ?? SqlString.Null);
                    command.Parameters.AddWithValue("@SpotifyAccountUrl", user.AccountUrl ?? SqlString.Null);
                    command.Parameters.AddWithValue("@SpotifyAuthenticationToken", user.SpotifyAuthenticationToken ?? SqlString.Null);
                    command.Parameters.AddWithValue("@SpotifyTokenExpire", user.SpotifyTokenExpire ?? SqlString.Null);
                    command.Parameters.AddWithValue("@SpotifyRefreshToken", user.SpotifyRefreshToken ?? SqlString.Null);
                    command.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth ?? SqlString.Null);
                    command.Parameters.AddWithValue("@DisplayName", user.DisplayName ?? SqlString.Null);
                    command.Parameters.AddWithValue("@FollowerCount", user.FollowerCount);

                    try
                    {
                        connection.Open();
                        var recordsAffected = command.ExecuteNonQuery();
                        Program.lm.Log(Program.lm.fileName, 3, user.UserId, "");
                        connection.Close();
                    }
                    catch (SqlException exception)
                    {
                        Program.lm.Log(Program.lm.fileName, -3, user.UserId, "");
                        // error here
                    }
                }
            }
        }
        public static void Updateuser(User user)
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();

            string query = "UPDATE Users SET " +

                  "UserId = \'" + user.UserId + "\' , " +
                  "SpotifyToken = \'" + user.SpotifyToken + "\' , " +
                  "UserRole = \'" + user.UserRole + "\' , " +
                  "Country = \'" + user.Country + "\' , " +
                  "SpotifyImgUrl = \'" + user.ImageUrl + "\' , " +
                  "SpotifyAccountUrl = \'" + user.AccountUrl + "\' , " +
                 "SpotifyAuthenticationToken = \'" + user.SpotifyAuthenticationToken + "\' , " +
                 "SpotifyTokenExpire = \'" + user.SpotifyTokenExpire + "\' , " +
                "SpotifyRefreshToken = \'" + user.SpotifyRefreshToken + "\' , " +
                "DateOfBirth = \'" + user.DateOfBirth + "\' , " +
                "DisplayName = \'" + user.DisplayName + "\' , " +
                "FollowerCount = \'" + user.FollowerCount + "\'" +
                "WHERE Id = " + user.UserId;
            var cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }
        public static List<User> ListUsers()
        {
            if (Con.State == ConnectionState.Open)
                Con.Close();
            var userList = new List<User>();
            Con.Open();
            var cmd = new SqlCommand("Select * From Users ", Con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                var newuser = new User
                {
                    Id = Convert.ToInt32(rd["Id"].ToString()),
                    UserId = rd["UserId"].ToString(),
                    DisplayName = rd["DisplayName"].ToString(),
                    AccountUrl = rd["SpotifyAccountUrl"].ToString(),
                    DateOfBirth = rd["DateOfBirth"].ToString(),
                    ImageUrl = rd["SpotifyImgUrl"].ToString(),
                    Country = rd["Country"].ToString(),
                    SpotifyAuthenticationToken = rd["SpotifyAuthenticationToken"].ToString(),
                    SpotifyRefreshToken = rd["SpotifyRefreshToken"].ToString(),
                    SpotifyToken = rd["SpotifyToken"].ToString(),
                    SpotifyTokenExpire = rd["SpotifyTokenExpire"].ToString(),
                    UserRole = Convert.ToInt32(rd["UserRole"].ToString()),
                    FollowerCount = Convert.ToInt32(rd["FollowerCount"].ToString())
                };
                userList.Add(newuser);
            }
            Con.Close();
            return userList;
        }
    }
}