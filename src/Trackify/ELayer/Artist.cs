using System;
using System.Collections.Generic;
using System.Linq;

namespace Trackify.ELayer
{
    public class Artist
    {
        public int Id { get; set; }
        public string ArtistURL { get; set; }
        public string ImageURL { get; set; }
        public string ArtistName { get; set; }
        public string ArtistSurname { get; set; }
        public int Popularity { get; set; }
        public string Genres  { get; set; }

    }
}