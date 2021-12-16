using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class VisionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
