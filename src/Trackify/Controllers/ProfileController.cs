using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trackify.ELayer;
using Trackify.FLayer;
using Trackify.SpotifyApi;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trackify.Controllers
{
    public class ProfileController : Controller
    {
        [HttpPost]
        public JsonResult printUser(User user)
        {
            user.DisplayName = user.DisplayName.Replace("\n", string.Empty);
            user.DisplayName = user.DisplayName.Trim(' ');

            User findUser = UserAdapter.GetUserByUserName(user.DisplayName);
            if (!string.IsNullOrEmpty(findUser.DisplayName))
                return Json(new { displayName = findUser.DisplayName });

            return Json(new { displayName = findUser.UserId });
        }

        [HttpPost]
        public async Task<JsonResult> getUsers(User userProfile)
        {
            try
            {
                ApiManager api = new ApiManager();
                string apiString = "https://api.spotify.com/v1/users/" + userProfile.DisplayName;
                string response = await api.GET(apiString);
                UserAdapter.ParseUserJson(response, 0);
                return Json(new { sit = "success" });
            }
            catch (Exception e)
            {
                return Json(new { sit = "fail" });
            }
        }


        public IActionResult ProfileScreen(string name)
        {
            User user = UserAdapter.GetUserByUserName(name);
            return View(user);
        }
    }
}
