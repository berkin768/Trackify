using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Trackify.ELayer
{
    public class User
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string SpotifyToken { get; set; }
        public int UserRole { get; set; }
        public string Country { get; set; }
        public string ImageUrl { get; set; }
        public string AccountUrl { get; set; }
        public string SpotifyAuthenticationToken { get; set; }
        public string SpotifyTokenExpire { get; set; }
        public string SpotifyRefreshToken { get; set; }
        public string DateOfBirth { get; set; }
        public string DisplayName { get; set; }
        public int FollowerCount { get; set; }
    }
}
