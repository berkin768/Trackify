using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trackify.ELayer;
using Trackify.FLayer;

namespace Trackify.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (!SpotifyApi.AuthenticationManager.Auth(Request))
            {
                return RedirectToAction("Unauthorized", "Login");
                
            }
            var currentUser = SpotifyApi.AuthenticationManager.GetUserFromCookie(Request);
            ViewData["Title"]=  currentUser.DisplayName;
            return View();
        }

        public IActionResult About()
        {

            return View();
        }
            
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
