using CinemaSystem.Data;
using CinemaSystem.Data.DTOs;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class VisionsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<VisionsController> _localizer;

     

        public VisionsController(ApplicationDbContext context, IStringLocalizer<VisionsController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        public IActionResult Index(string culture)
        {
            
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture


            if (culture != null)
            {
                var cultureInfo = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            var selectedCulture = rqf.RequestCulture.Culture;

            // LOCALIZATION //
            ViewData["ctr"] = selectedCulture;
            ViewData["Vision"] = _localizer["Deneme"];
            ViewData["MenuHome"] = _localizer["MenuHome"];
            ViewData["MenuContact"] = _localizer["MenuContact"];
            ViewData["MenuVisions"] = _localizer["MenuVisions"];
            ViewData["ContactUs"] = _localizer["ContactUs"];
            ViewData["MenuProfile"] = _localizer["MenuProfile"];
            ViewData["PageFirstTitle"] = _localizer["PageFirstTitle"];
            ViewData["ClickForMovieDetail"] = _localizer["ClickForMovieDetail"];
            ViewData["VisionPagePromotionCode"] = _localizer["VisionPagePromotionCode"];
            // LOCALIZATION //




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
