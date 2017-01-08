using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Trackify.ELayer;
using Trackify.FLayer;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trackify.Controllers
{
    public class AdminController : Controller
    {
        public JsonResult deleteUser(User user)
        {
            try
            {
                Console.WriteLine(user.DisplayName);
                User findUser = UserAdapter.GetUserByUserName(user.DisplayName);
                if (!string.IsNullOrEmpty(findUser.DisplayName))
                {
                    Program.users.RemoveAll(x => x.DisplayName == findUser.DisplayName);
                    UserAdapter.RemoveUser(findUser);
                }
                else
                {
                    Program.users.RemoveAll(x => x.UserId == findUser.UserId);
                    UserAdapter.RemoveUser(findUser);
                }
                Program.lm.Log(Program.lm.fileName, 1, user.UserId, "");
                return Json(new { sit = "success" });
            }
            catch (Exception e)
            {
                Program.lm.Log(Program.lm.fileName, -1, user.UserId, "");
                return Json(new { sit = "fail" });
            }
        }

        public JsonResult updateUser(User user)
        {
            if (!string.IsNullOrEmpty(user.DisplayName))
                return Json(new { sit = "update", displayName = user.DisplayName });
            return Json(new { sit = "update", displayName = user.UserId });
        }

        public void confirmUpdate(User user)
        {
            var updatedUser = Program.users.First(s => s.Id == user.Id);
            string tempDate = updatedUser.UserId;
            try
            {
                UserAdapter.RemoveUser(updatedUser);
                updatedUser.DisplayName = user.DisplayName;
                updatedUser.Country = user.Country;
                updatedUser.DateOfBirth = user.DateOfBirth;
                updatedUser.UserId = user.UserId;
                updatedUser.FollowerCount = user.FollowerCount;
                Program.lm.Log(Program.lm.fileName, 2, tempDate, updatedUser.UserId);
                UserAdapter.AddUser(updatedUser);
            }
            catch (Exception e)
            {
                Program.lm.Log(Program.lm.fileName, -2, user.UserId, "?");
            }

        }

        public IActionResult UpdatePage(string name)
        {
            User findUser = UserAdapter.GetUserByUserName(name);
            return View(findUser);
        }

        public IActionResult AddUser()
        {
            return View();
        }

        // GET: /<controller>/
        public IActionResult AdminPage()
        {
            var users = Program.users.AsEnumerable();
            return View(users);
        }

    }
}
