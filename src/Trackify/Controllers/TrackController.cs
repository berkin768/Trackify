﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Trackify.Controllers
{
    public class TrackController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index(string TrackId)
        {
            return View();
        }
    }
}
