using CinemaSystem.Data;
using CinemaSystem.Data.DTOs;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var visionFilmList = (from ctm in _context.CinemaTheaterMovies
                                  join m in _context.Movies on ctm.MovieId equals m.Id
                                  select new MovieDTO
                                  {
                                      MovieID = m.Id,
                                      MovieName=m.MovieName,
                                      Year=m.Year,
                                      MovieLength=m.Time,
                                      Subject=m.Subject,
                                      StartedDate=ctm.StartedDate,
                                      FinishedDate=ctm.FinishedDate,
                                      Category=m.Category.CategoryName,
                                      Poster=m.Poster,
                                      Fragment=m.Fragment,
                                      IMDB_Puan=m.IMDB_Puan
                                  }).OrderBy(x=>x.StartedDate).ToList().Where(x=>x.FinishedDate>=DateTime.Now.Date).Take(3);



            ViewBag.movies = visionFilmList;

           ViewBag.categories = _context.Categories.ToList(); ;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Deneme()
        {
            ViewBag.movies = _context.Movies
                 .Include(m => m.Language)
                 .Include(m => m.Category).Take(3).ToList();

            ViewBag.categories = _context.Categories.ToList(); ;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
