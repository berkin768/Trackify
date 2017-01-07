using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trackify.ELayer
{
    public class Track
    {
        public int Id { get; set; }
        public string TrackId { get; set; }
        public string ImageUrl { get; set; }
        public string TrackUrl { get; set; }
        public string TrackName { get; set; }
        public int TrackLength { get; set; }
        public int Popularity { get; set; }
        public string AlbumId { get; set; }
        public string ArtistId { get; set; }
        public string PlaylistId { get; set; }
    }
}
