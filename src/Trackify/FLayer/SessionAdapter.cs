using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Trackify.ELayer;

namespace Trackify.FLayer
{
    public class SessionAdapter
    {
        private static SqlConnection Con = new SqlConnection(Program.ConnectionString);
        public static Session GenerateSession(int UserId)
        {
            Session s;
            if(GetSessionByUserId(UserId) != null)
            {
                s = UpdateSession(UserId);
            }
            else
            {
                s = new Session();
                s.UserId = UserId;
                s.Code = GenerateRandomCode();
                SaveSession(s);
            }
            return s;
        }
        public static void SaveSession(Session s){
            using (SqlConnection connection = new SqlConnection(Program.ConnectionString))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;            // <== lacking
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        " INSERT INTO SessionTBL (UserId, Code)" +
                        "VALUES(@UserId, @Code)";
                    command.Parameters.AddWithValue("@UserId", s.UserId);
                    command.Parameters.AddWithValue("@Code", s.Code ?? SqlString.Null);
                    try
                    {
                        connection.Open();
                        int recordsAffected = command.ExecuteNonQuery();
                        connection.Close();
                    }
                    catch (SqlException exception)
                    {
                        Console.WriteLine(exception.StackTrace);
                    }
                }
            }
        }
        public static Session GetSessionByCode(string Code)
        {
            Session s = new Session();
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            var cmd = new SqlCommand("Select * From SessionTBL Where Code = \'" + Code + "\';", Con);
            var rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                Con.Close();
                return null;
            }
            s.Id = Convert.ToInt32(rd["Id"].ToString());
            s.UserId = Convert.ToInt32(rd["UserId"].ToString());
            s.Code = rd["Code"].ToString();
            Con.Close();
            return s;
        }
        public static Session GetSessionByUserId(int UserId)
        {
            Session s = new Session();
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            var cmd = new SqlCommand("Select * From SessionTBL Where UserId = \'" + UserId + "\';", Con);
            var rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                Con.Close();
                return null;
            }
            s.Id = Convert.ToInt32(rd["Id"].ToString());
            s.UserId = Convert.ToInt32(rd["UserId"].ToString());
            s.Code = rd["Code"].ToString();
            Con.Close();
            return s;
        }
        public static Session UpdateSession(int UserId)
        {
            Session s = new ELayer.Session();
            s.UserId = UserId;
            s.Code = GenerateRandomCode();
            if (Con.State == ConnectionState.Open)
                Con.Close();
            Con.Open();
            string query = "UPDATE SessionTBL SET " +
                "UserId = \'" + s.UserId + "\' , " +
                "Code = \'" + s.Code + "\' " +
                "WHERE UserId = \'" + s.UserId + "\';"; 
            var cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
            return s;
        }
        //if returns null this means it does not exist
        public static int ValidateUser(string CookieCode)
        {
            Session s = GetSessionByCode(CookieCode);
            if (s != null){
                return s.UserId;
            }
            return -1;
        }
        private static string GenerateRandomCode()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
