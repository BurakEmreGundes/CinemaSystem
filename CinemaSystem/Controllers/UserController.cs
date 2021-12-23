using CinemaSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Tickets()
        {
            var tickets = await _context.MovieTickets
                .Include(c => c.MovieSession)
                .Include(c => c.TheaterChair)
                .Include(c=>c.MovieSession.CinemaTheaterMovie)
                .Include(c => c.MovieSession.CinemaTheaterMovie.CinemaTheater)
                .Include(c=>c.MovieSession.CinemaTheaterMovie.Movie)
         
                .Where(x=>x.UserId==TempData["UserId"].ToString()).ToListAsync();

            // buna bir dto yap

            ViewBag.tickets = tickets;
            return View();
        }
        // GET: MovieTickets/Delete/5
        public async Task<IActionResult> DeleteTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTicket = await _context.MovieTickets
                .Include(m => m.MovieSession)
                .Include(m => m.TheaterChair)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieTicket == null)
            {
                return NotFound();
            }

            return View(movieTicket);
        }
    }
}
