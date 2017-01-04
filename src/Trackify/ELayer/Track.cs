using System;
using System.Collections.Generic;
using System.Linq;

namespace Trackify
{
    public class Track
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string TrackURL { get; set; }
        public string TrackName { get; set; }
        public int TrackLength { get; set; }
        public int Popularity { get; set; }
        public int AlbumId { get; set; }
        public int ArtistId { get; set; }
        //Need revision of this field
        public int PlaylistId { get; set; }


    }
}