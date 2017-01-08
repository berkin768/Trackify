using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trackify.ELayer;
using Trackify.FLayer;

namespace Trackify.SpotifyApi
{
    public class AuthenticationManager
    {
        public static bool Auth(HttpRequest request)
        {
            if (request.Cookies["Trackify-Auth"] != null)
            {
                if(request.Cookies["Trackify-UId"] != null)
                {
                    int tempUid = SessionAdapter.ValidateUser((request.Cookies["Trackify-Auth"]));
                    if (tempUid.ToString().Equals(request.Cookies["Trackify-UId"]))
                    {
                        Console.WriteLine("AUTHENTICATED");
                        return true;
                    }
                  
                }
            }
            return false;
        }

        public static User GetUserFromCookie(HttpRequest request)
        {
            int uid = Convert.ToInt32(request.Cookies["Trackify-UId"]);
            return UserAdapter.GetUserById(uid);
           
        }
    }
}
