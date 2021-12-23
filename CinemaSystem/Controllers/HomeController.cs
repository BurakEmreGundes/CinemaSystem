using CinemaSystem.Data;
using CinemaSystem.Data.DTOs;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ApplicationDbContext context, IStringLocalizer<HomeController> localizer)
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

            ViewData["ctr"] = selectedCulture;

            ViewData["deneme"] = _localizer["Deneme"];

            // LOCALIZATION //
            ViewData["HomePageFirstBoxTitle"] = _localizer["HomePageFirstBoxTitle"];
            ViewData["HomePageFirstBoxDesc"] = _localizer["HomePageFirstBoxDesc"];
            ViewData["HomePageSecondBoxTitle"] = _localizer["HomePageSecondBoxTitle"];
            ViewData["HomePageSecondBoxDesc"] = _localizer["HomePageSecondBoxDesc"];
            ViewData["HomePageThirdBoxTitle"] = _localizer["HomePageThirdBoxTitle"];
            ViewData["HomePageThirdBoxDesc"] = _localizer["HomePageThirdBoxDesc"];
            ViewData["PageFirstTitle"] = _localizer["PageFirstTitle"];
            ViewData["ClickForMovieDetail"] = _localizer["ClickForMovieDetail"];
            ViewData["SeeAllVisions"] = _localizer["SeeAllVisions"];
            ViewData["HomePageSupport"] = _localizer["HomePageSupport"];
            ViewData["HomePageSupportDesc"] = _localizer["HomePageSupportDesc"];
            ViewData["HomePagePromotionCode"] = _localizer["HomePagePromotionCode"];
            ViewData["MenuHome"] = _localizer["MenuHome"];
            ViewData["MenuContact"] = _localizer["MenuContact"];
            ViewData["MenuVisions"] = _localizer["MenuVisions"];
            ViewData["ContactUs"] = _localizer["ContactUs"];
            ViewData["MenuProfile"] = _localizer["MenuProfile"];
            // LOCALIZATION //


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
