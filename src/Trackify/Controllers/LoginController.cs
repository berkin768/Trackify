using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Trackify.FLayer;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trackify.Controllers
{
    public class LoginController : Controller
    {
        private static string client_id = "5f257bfd7fff42e68a6ed07a8b43ec74";
        private static string client_secret = "fc32d069cd754791b23dd1fdeaa432d9";
        private static string redirect_uri = "http://localhost:46607/login/callback";
        // GET: /<controller>/
        public IActionResult Index()
        {
            string response = "https://accounts.spotify.com/authorize";
            
            response = response + "?client_id="+client_id+"&redirect_uri="+redirect_uri+"&scope=user-read-private%20user-read-email&response_type=code&state=123";
            return Redirect(response);
        }
        public async Task<string> Callback(string code, string state, string error)
        {
            if (error != null)
            {
                return "Auth Error Accoured!";

            }
            CallbackResponse cr;
            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                   { "client_id", client_id },
                   { "client_secret", client_secret },
                   { "grant_type", "authorization_code" },
                   { "code", code },
                   { "redirect_uri", redirect_uri }
                };
                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync("https://accounts.spotify.com/api/token", content);

                var responseString = await response.Content.ReadAsStringAsync();
                cr = JsonConvert.DeserializeObject<CallbackResponse>(responseString);
                
            }

            var s = SessionAdapter.GenerateSession("ALLAH");
            //TODO Get user details
            //Add user if not exist on table
            //Give User a cookie
            //Save that cookie on a table
            return s.Code; 
            
        }
        class CallbackResponse {
            public string access_token;
            public string token_type;
            public int expires_in;
            public string refresh_token;
            public string scope;
        }
    }
}
