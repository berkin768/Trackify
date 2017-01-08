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
        public void deleteUsers()
        {
            UserAdapter.RemoveAllUsers();
        }

        public IEnumerable<User> getUsers()
        {
            Program.users = UserAdapter.ListUsers();
            return Program.users.AsEnumerable();
        }

        public IActionResult Index()
        {
            if (!SpotifyApi.AuthenticationManager.Auth(Request))
            {
                return RedirectToAction("Unauthorized", "Login");
            }
            var currentUser = SpotifyApi.AuthenticationManager.GetUserFromCookie(Request);
            ViewData["Title"] = currentUser.DisplayName;
            var users = getUsers();
            return View(users);
        }
    }
}
