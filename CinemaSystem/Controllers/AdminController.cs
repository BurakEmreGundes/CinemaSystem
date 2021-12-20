using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }
    }
}
