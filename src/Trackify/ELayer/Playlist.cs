namespace Trackify.ELayer
{
    public class Playlist
    {
        public int Id { get; set; }
        public string PlaylistId { get; set; }
        public string PlaylistUrl { get; set; }
        public int Downvote { get; set; }
        public int Upvote { get; set; }
        public string UserId { get; set; }
    }
}
