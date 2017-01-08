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
    public class TrackAdapter
    {
        static SqlConnection con = new SqlConnection(Program.ConnectionString);
        public static Track GetTrackById(string TrackId)
        {
            Track newtrack = new Track();

            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Select * From Tracks Where TrackID = " + TrackId, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                return null;
            }
            newtrack.Id = Convert.ToInt32(rd["Id"].ToString());
            newtrack.TrackId = rd["TrackID"].ToString();
            newtrack.ImageUrl = rd["ImageURL"].ToString();
            newtrack.TrackUrl = rd["TrackURL"].ToString();
            newtrack.TrackName = rd["TrackName"].ToString();
            newtrack.TrackLength = Convert.ToInt32(rd["TrackLength"].ToString());
            newtrack.Popularity = Convert.ToInt32(rd["Popularity"].ToString());
            newtrack.AlbumId = rd["AlbumId"].ToString();
            newtrack.ArtistId = rd["ArtistId"].ToString();
            newtrack.PlaylistId = rd["PlaylistId"].ToString();
            return newtrack;
        }
        public static Track GetByTrackName(string trackName)
        {
            Track newtrack = new Track();
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Select * From Tracks Where TrackName = " + trackName, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                con.Close();
                return null;
            }
            newtrack.Id = Convert.ToInt32(rd["Id"].ToString());
            newtrack.TrackId = rd["TrackID"].ToString();
            newtrack.ImageUrl = rd["ImageURL"].ToString();
            newtrack.TrackUrl = rd["TrackURL"].ToString();
            newtrack.TrackName = rd["TrackName"].ToString();
            newtrack.TrackLength = Convert.ToInt32(rd["TrackLength"].ToString());
            newtrack.Popularity = Convert.ToInt32(rd["Popularity"].ToString());
            newtrack.AlbumId = rd["AlbumId"].ToString();
            newtrack.ArtistId = rd["ArtistId"].ToString();
            newtrack.PlaylistId = rd["PlaylistId"].ToString();
            return newtrack;
        }
        public static void RemoveTrack(Track track)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Delete From Tracks Where TrackID = " + track.TrackId, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void AddTrack(Track track)
        {
            using (var connection = new SqlConnection(Program.ConnectionString))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        " INSERT INTO Track (TrackID, ImageURL, TrackURL, TrackName, TrackLength, Popularity, AlbumId, ArtistId," +
                        " PlaylistId)" +

                        "VALUES(@TrackID, @ImageURL, @TrackURL, @TrackName, @TrackLength, @Popularity, @AlbumId, " +
                        "@ArtistId, @PlaylistId)";
                    command.Parameters.AddWithValue("@TrackID", track.TrackId ?? SqlString.Null);
                    command.Parameters.AddWithValue("@ImageURL", track.ImageUrl ?? SqlString.Null);
                    command.Parameters.AddWithValue("@TrackURL", track.TrackUrl ?? SqlString.Null);
                    command.Parameters.AddWithValue("@TrackName", track.TrackName ?? SqlString.Null);
                    command.Parameters.AddWithValue("@TrackLength", track.TrackLength);
                    command.Parameters.AddWithValue("@Popularity", track.Popularity);
                    command.Parameters.AddWithValue("@AlbumId", track.AlbumId ?? SqlString.Null);
                    command.Parameters.AddWithValue("@ArtistId", track.ArtistId ?? SqlString.Null);
                    command.Parameters.AddWithValue("@PlaylistId", track.PlaylistId ?? SqlString.Null);

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
        public static void UpdateTrack(Track track)
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            con.Open();
            string query = "UPDATE Tracks SET " +
                "TrackID = \'" + track.TrackId + "\' " +
                "ImageURL = \'" + track.ImageUrl + "\' , " +
                "TrackURL = \'" + track.TrackUrl + "\' , " +
                "TrackName = \'" + track.TrackName + "\' , " +
                "TrackLength = \'" + track.TrackLength + "\' , " +
                "Popularity = \'" + track.Popularity + "\' , " +
                "AlbumId = \'" + track.AlbumId + "\' , " +
                "ArtistId = \'" + track.ArtistId + "\' , " +
                "PlaylistId = \'" + track.PlaylistId + "\' " +
                "WHERE Id = " + track.Id;
            var cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static List<Track> ListTracks()
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            var trackList = new List<Track>();
            con.Open();
            var cmd = new SqlCommand("Select * From Tracks ", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Track newtrack = new Track
                {
                    Id = Convert.ToInt32(rd["Id"].ToString()),
                    AlbumId = rd["AlbumId"].ToString(),
                    ArtistId = rd["ArtistId"].ToString(),
                    TrackUrl = rd["TrackURL"].ToString(),
                    ImageUrl = rd["ImageURL"].ToString(),
                    TrackName = rd["TrackName"].ToString(),
                    TrackLength = Convert.ToInt32(rd["TrackLength"].ToString()),
                    PlaylistId = rd["PlaylistId"].ToString(),
                    Popularity = Convert.ToInt32(rd["Popularity"].ToString())
                };
                trackList.Add(newtrack);
            }
            con.Close();
            return trackList; //TrackList;
        }
    }
}
