﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Trackify;
using Trackify.ELayer;

namespace Trackify.FLayer
{
    public class UserAdapter
    {
        private static SqlConnection Con = new SqlConnection(Program.ConnectionString);
        public static User GetUserById(string userId)
        {
            User newuser = new User();
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            var cmd = new SqlCommand("Select * From Users Where UserId = " + userId, Con);
            var rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                return null;
            }
            newuser.Id = Convert.ToInt32(rd["Id"].ToString());
            newuser.UserId = rd["UserId"].ToString();
            newuser.DisplayName = rd["DisplayName"].ToString();
            newuser.AccountUrl = rd["AccountUrl"].ToString();
            newuser.DateOfBirth = rd["DateOfBirth"].ToString();
            newuser.ImageUrl = rd["ImageUrl"].ToString();
            newuser.Country = rd["Country"].ToString();
            newuser.SpotifyAuthenticationToken = rd["SpotifyAuthenticationToken"].ToString();
            newuser.SpotifyRefreshToken = rd["SpotifyRefreshToken"].ToString();
            newuser.SpotifyToken = rd["SpotifyToken"].ToString();
            newuser.SpotifyTokenExpire = rd["SpotifyTokenExpire"].ToString();
            newuser.UserRole = Convert.ToInt32(rd["UserRole"].ToString());
            newuser.FollowerCount = Convert.ToInt32(rd["FollowerCount"].ToString());
            return newuser;
        }

        public static User GetUserByUserName(string displayName)
        {
            User newuser = new User();
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            var cmd = new SqlCommand("Select * From Users Where DisplayName = " + displayName, Con);
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
            var cmd = new SqlCommand("Delete From Users Where UserId = " + user.Id, Con);
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

                        "VALUES(@UserId, @SpotifyToken, @UserRole, @Country, @ImageUrl, @AccountUrl, @SpotifyAuthenticationToken, " +
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
                        connection.Close();
                    }
                    catch (SqlException exception)
                    {
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
                "FollowerCount = \'" + user.FollowerCount + "\' , " +
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
                    AccountUrl = rd["AccountUrl"].ToString(),
                    DateOfBirth = rd["DateOfBirth"].ToString(),
                    ImageUrl = rd["ImageUrl"].ToString(),
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