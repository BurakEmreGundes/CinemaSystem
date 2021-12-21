using CinemaSystem.Data;
using CinemaSystem.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class VisionsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public VisionsController(ApplicationDbContext context)
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
                                      MovieName = m.MovieName,
                                      Year = m.Year,
                                      MovieLength = m.Time,
                                      Subject = m.Subject,
                                      StartedDate = ctm.StartedDate,
                                      FinishedDate = ctm.FinishedDate,
                                      Category = m.Category.CategoryName,
                                      Poster = m.Poster,
                                      Fragment = m.Fragment,
                                      IMDB_Puan = m.IMDB_Puan
                                  }).OrderBy(x => x.StartedDate).ToList().Where(x => x.FinishedDate >= DateTime.Now.Date);



            ViewBag.movies = visionFilmList;

            ViewBag.categories = _context.Categories.ToList(); 

            return View();
        }
    }
}
