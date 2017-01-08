using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Trackify.FLayer;
using Trackify.ELayer;

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
            
            response = response + "?client_id="+client_id+"&redirect_uri="+redirect_uri+ "&scope=user-read-private%20user-read-email%20user-read-birthdate&response_type=code&state=123";
            return Redirect(response);
        }
        public async Task<IActionResult> Callback(string code, string state, string error)
        {
            if (error != null)
            {
                return RedirectToRoute("/login/unauthorized");

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
            string responseJson = await SpotifyApi.ApiManager.Auth_Get("https://api.spotify.com/v1/me",cr.access_token);
            Console.WriteLine(responseJson);
            //save user on sign-up
            User CurrentUser = UserAdapter.ParseUserJson(responseJson);
            //generate and save session information
            CurrentUser = UserAdapter.GetUserBySpotifyUserId(CurrentUser.UserId);
            Session s = SessionAdapter.GenerateSession(CurrentUser.Id);
            //Set the cookies value
            Response.Cookies.Append("Trackify-Auth",s.Code);
            Response.Cookies.Append("Trackify-UId", s.UserId.ToString());
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Unauthorized()
        {
            return View();
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
