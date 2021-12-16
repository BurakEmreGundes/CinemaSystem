using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaSystem.Data;
using CinemaSystem.Models;

namespace CinemaSystem.Controllers
{
    public class MovieTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieTicketsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieTickets
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieTickets.Include(m => m.MovieSession).Include(m => m.TheaterChair);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieTickets/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: MovieTickets/Create
        public IActionResult Create()
        {
            ViewData["MovieSessionId"] = new SelectList(_context.MovieSessions, "Id", "Id");
            ViewData["TheaterChairId"] = new SelectList(_context.TheaterChairs, "Id", "Id");
            return View();
        }

        // POST: MovieTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieSessionId,BuyDate,Price,Number,TheaterChairId")] MovieTicket movieTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieSessionId"] = new SelectList(_context.MovieSessions, "Id", "Id", movieTicket.MovieSessionId);
            ViewData["TheaterChairId"] = new SelectList(_context.TheaterChairs, "Id", "Id", movieTicket.TheaterChairId);
            return View(movieTicket);
        }

        // GET: MovieTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieTicket = await _context.MovieTickets.FindAsync(id);
            if (movieTicket == null)
            {
                return NotFound();
            }
            ViewData["MovieSessionId"] = new SelectList(_context.MovieSessions, "Id", "Id", movieTicket.MovieSessionId);
            ViewData["TheaterChairId"] = new SelectList(_context.TheaterChairs, "Id", "Id", movieTicket.TheaterChairId);
            return View(movieTicket);
        }

        // POST: MovieTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieSessionId,BuyDate,Price,Number,TheaterChairId")] MovieTicket movieTicket)
        {
            if (id != movieTicket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieTicketExists(movieTicket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieSessionId"] = new SelectList(_context.MovieSessions, "Id", "Id", movieTicket.MovieSessionId);
            ViewData["TheaterChairId"] = new SelectList(_context.TheaterChairs, "Id", "Id", movieTicket.TheaterChairId);
            return View(movieTicket);
        }

        // GET: MovieTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: MovieTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieTicket = await _context.MovieTickets.FindAsync(id);
            _context.MovieTickets.Remove(movieTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieTicketExists(int id)
        {
            return _context.MovieTickets.Any(e => e.Id == id);
        }
    }
}
