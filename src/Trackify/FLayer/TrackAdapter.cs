using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Trackify.FLayer
{
    public class TrackAdapter
    {
        static SqlConnection con = new SqlConnection(Program.ConnectionString);
        public static Track GetTrackById(int Id)
        {
            Track newtrack = new Track();
            string tmpId = Id.ToString();
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Select * From Tracks Where Id = " + tmpId, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                return null;
            }
            newtrack.Id = Convert.ToInt32(rd["Id"].ToString());
            newtrack.AlbumId = Convert.ToInt32(rd["AlbumId"].ToString());
            newtrack.ArtistId = Convert.ToInt32(rd["ArtistId"].ToString());
            newtrack.TrackURL = rd["TrackURL"].ToString();
            newtrack.ImageURL = rd["ImageURL"].ToString();
            newtrack.TrackName = rd["TrackName"].ToString();
            newtrack.TrackLength = Convert.ToInt32(rd["TrackLength"].ToString());
            newtrack.PlaylistId = Convert.ToInt32(rd["PlaylistId"].ToString());
            newtrack.Popularity = Convert.ToInt32(rd["Popularity"].ToString());
            return newtrack;
        }
        public static Track GetByTrackName(string TrackName)
        {
            Track newtrack = new Track();
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Select * From Tracks Where TrackName = " + TrackName, con);
            SqlDataReader rd = cmd.ExecuteReader();
            if (!rd.Read())
            {
                con.Close();
                return null;
            }
            newtrack.Id = Convert.ToInt32(rd["Id"].ToString());
            newtrack.AlbumId = Convert.ToInt32(rd["AlbumId"].ToString());
            newtrack.ArtistId = Convert.ToInt32(rd["ArtistId"].ToString());
            newtrack.TrackURL = rd["TrackURL"].ToString();
            newtrack.ImageURL = rd["ImageURL"].ToString();
            newtrack.TrackName = rd["TrackName"].ToString();
            newtrack.TrackLength = Convert.ToInt32(rd["TrackLength"].ToString());
            newtrack.PlaylistId = Convert.ToInt32(rd["PlaylistId"].ToString());
            newtrack.Popularity = Convert.ToInt32(rd["Popularity"].ToString());
            return newtrack;
        }
        public static void RemoveTrack(Track track)
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            var cmd = new SqlCommand("Delete From Tracks Where Id = " + track.Id.ToString(), con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void AddTrack(Track track)
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            string query = "Insert Into Tracks(Id, ImageURL, TrackURL, TrackName, TrackLength, Popularity, AlbumId, ArtistId, PlaylistId) values(" +
                "\'" + track.Id + "\' , " +
                "\'" + track.ImageURL + "\' , " +
                "\'" + track.TrackURL + "\' , " +
                "\'" + track.TrackName + "\' , " +
                "\'" + track.TrackLength + "\' , " +
                "\'" + track.Popularity + "\' , " +
                "\'" + track.AlbumId + "\' , " +
                "\'" + track.ArtistId + "\' , " +
                "\'" + track.PlaylistId + "\');";
            var cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public static void UpdateTrack(Track track)
        {
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            string query = "UPDATE Tracks SET " +
                "ImageURL = \'" + track.ImageURL + "\' , " +
                "TrackURL = \'" + track.TrackURL + "\' , " +
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
            List<Track> TrackList = new List<Track>();
            con.Open();
            var cmd = new SqlCommand("Select * From Tracks ", con);
            SqlDataReader rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                Track newtrack = new Track();
                newtrack.Id = Convert.ToInt32(rd["Id"].ToString());
                newtrack.AlbumId = Convert.ToInt32(rd["AlbumId"].ToString());
                newtrack.ArtistId = Convert.ToInt32(rd["ArtistId"].ToString());
                newtrack.TrackURL = rd["TrackURL"].ToString();
                newtrack.ImageURL = rd["ImageURL"].ToString();
                newtrack.TrackName = rd["TrackName"].ToString();
                newtrack.TrackLength = Convert.ToInt32(rd["TrackLength"].ToString());
                newtrack.PlaylistId = Convert.ToInt32(rd["PlaylistId"].ToString());
                newtrack.Popularity = Convert.ToInt32(rd["Popularity"].ToString());
                TrackList.Add(newtrack);
            }
            con.Close();
            return TrackList;
        }
}
}
