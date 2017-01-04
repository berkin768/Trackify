using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trackify.Controllers
{
    public class ProfileController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(int id)
        {
            Console.WriteLine("IDID =" + id);
            //User usr = UserAdapter.GetUserById(id);
            ViewData["Id"] = id;
            //ViewData["targetUser"] = usr;
            //get and add to ViewController listened songs and playlits here
            return View();
        }
    }
}
