using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class MovieTheatersPageController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MovieTheatersPageController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTheatersByMovieId = _context.CinemaTheaterMovies.Include(c => c.CinemaTheater).Include(c => c.Movie);
            ViewBag.id = id;
            ViewBag.ctbymi = await cinemaTheatersByMovieId.Where(x=>x.MovieId==id && x.FinishedDate>DateTime.Now.Date).ToListAsync();
            return View();
        }
        public async Task<IActionResult> MovieSessions(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieSessionsByMovieTheaterId = _context.MovieSessions.Include(c => c.CinemaTheaterMovie);
            ViewBag.msbyti = await movieSessionsByMovieTheaterId.Where(x=>x.CinemaTheaterMovieId==id && x.FinishedDate>=DateTime.Now).ToListAsync();
            return View();

        }

        public async Task<IActionResult> TheaterChairs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
