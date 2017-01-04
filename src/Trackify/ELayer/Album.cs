using System;
using System.Collections.Generic;
using System.Linq;

namespace Trackify.ELayer
{
    public class Album
    {
        public int Id { get; set; }
        public string AlbumURL { get; set; }
        public string ImageURL { get; set; }
        public string AlbumName { get; set; }
        public Date ReleaseDate { get; set; }
        public int Popularity { get; set; }
        public int ArtistId { get; set; }

    }
}