namespace Trackify.ELayer
{
    public class Album
    {
        public int Id { get; set; }
        public string AlbumId { get; set; }
        public string AlbumUrl { get; set; }
        public string ImageUrl { get; set; }
        public string AlbumName { get; set; }
        public string ReleaseDate { get; set; }
        public int Popularity { get; set; }
        public int ArtistId { get; set; }
    }
}
