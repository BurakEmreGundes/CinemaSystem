using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class MovieTheatersPageController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly IStringLocalizer<MovieTheatersPageController> _localizer;

        public MovieTheatersPageController(ApplicationDbContext context, IStringLocalizer<MovieTheatersPageController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }
        [Authorize]
        public async Task<IActionResult> Index(int? id,string culture)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture


            if (culture != null)
            {
                var cultureInfo = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            var selectedCulture = rqf.RequestCulture.Culture;

            ViewData["ctr"] = selectedCulture;

            ViewData["ContactUs"] = _localizer["ContactUs"];
            ViewData["MenuContact"] = _localizer["MenuContact"];
            ViewData["MenuHome"] = _localizer["MenuHome"];
            ViewData["MenuProfile"] = _localizer["MenuProfile"];
            ViewData["MenuVisions"] = _localizer["MenuVisions"];
            ViewData["PageFirstTitle"] = _localizer["PageFirstTitle"];
            ViewData["PagePromotionCode"] = _localizer["PagePromotionCode"];
            ViewData["TheaterNo"] = _localizer["TheaterNo"];

            ViewBag.movieIdForGetSession=id;



            var cinemaTheatersByMovieId = _context.CinemaTheaterMovies.Include(c => c.CinemaTheater).Include(c => c.Movie);
            ViewBag.id = id;
            ViewBag.ctbymi = await cinemaTheatersByMovieId.Where(x=>x.MovieId==id && x.FinishedDate>DateTime.Now.Date).ToListAsync();
            return View();
        }
        [Authorize]
        public async Task<IActionResult> MovieSessions(int? id,string? movieId,string culture)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture


            if (culture != null)
            {
                var cultureInfo = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            var selectedCulture = rqf.RequestCulture.Culture;

            ViewData["ctr"] = selectedCulture;

            ViewData["ContactUs"] = _localizer["ContactUs"];
            ViewData["MenuContact"] = _localizer["MenuContact"];
            ViewData["MenuHome"] = _localizer["MenuHome"];
            ViewData["MenuProfile"] = _localizer["MenuProfile"];
            ViewData["MenuVisions"] = _localizer["MenuVisions"];
            ViewData["PageFirstTitle"] = _localizer["PageFirstTitle"];
            ViewData["PagePromotionCode"] = _localizer["PagePromotionCode"];
            ViewData["MovieSessionPageFirstTitle"] = _localizer["MovieSessionPageFirstTitle"];
            ViewData["MovieSessionPageButton"] = _localizer["MovieSessionPageButton"];



            //MOVİE ID YE GÖRE DE KONTROL OLACAK
            // 
            

            var movieSessionsByMovieTheaterId = _context.MovieSessions.Include(c => c.CinemaTheaterMovie);
            ViewBag.msbyti = await movieSessionsByMovieTheaterId.Where(x=>x.CinemaTheaterMovieId==id && x.FinishedDate>=DateTime.Now && x.CinemaTheaterMovie.MovieId.ToString()==movieId).ToListAsync();
            return View();

        }
        [Authorize]
        public async Task<IActionResult> TheaterChairs(int? id,string culture)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture


            if (culture != null)
            {
                var cultureInfo = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            var selectedCulture = rqf.RequestCulture.Culture;

            ViewData["ctr"] = selectedCulture;

            ViewData["ContactUs"] = _localizer["ContactUs"];
            ViewData["MenuContact"] = _localizer["MenuContact"];
            ViewData["MenuHome"] = _localizer["MenuHome"];
            ViewData["MenuProfile"] = _localizer["MenuProfile"];
            ViewData["MenuVisions"] = _localizer["MenuVisions"];
            ViewData["PageFirstTitle"] = _localizer["PageFirstTitle"];
            ViewData["PagePromotionCode"] = _localizer["PagePromotionCode"];
            ViewData["TheaterChairsPageChairNo"] = _localizer["TheaterChairsPageChairNo"];
            ViewData["TheaterChairsPageCreateButton"] = _localizer["TheaterChairsPageCreateButton"];


            var movieSessions = _context.MovieSessions.Include(c => c.CinemaTheaterMovie);
            var cinemaMovieSession = await movieSessions.SingleOrDefaultAsync(x => x.Id == id);

            var theaterChairs = await _context.TheaterChairs.Include(c => c.CinemaTheater)
                .Where(x=>x.CinemaTheaterId==cinemaMovieSession.CinemaTheaterMovie.CinemaTheaterId).ToListAsync();

            ViewBag.movieSessionId = id;
            ViewBag.theaterChairs = theaterChairs;

            var movieTickets = await _context.MovieTickets.Include(c => c.MovieSession).Include(c => c.TheaterChair)
                .Where(x => x.MovieSessionId == id).ToListAsync();

            var movieTicketsChair=new List<int>();

            foreach (var item in movieTickets)
            {
                movieTicketsChair.Add(item.TheaterChairId);
            }


            ViewBag.movieTicketsChair = movieTicketsChair;

            //biletleri listele bu seansa olan 
            // biletlerin içerdiği koltukları kırmızı yap gerisi alınabilsin.
         
            return View();


        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTicket([Bind("Id,MovieSessionId,BuyDate,Price,Number,UserId,TheaterChairId")] MovieTicket movieTicket)
        {

            if(ModelState.IsValid)
            {
                _context.Add(movieTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Visions");
            }

            return RedirectToAction("Index", "Visions");
            //Movie session id 
            //Koltuk id -> select den seçtir


        }
    }
}
