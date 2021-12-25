using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace CinemaSystem.Controllers
{
    public class MovieSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: MovieSessions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieSessions.Include(m => m.CinemaTheaterMovie);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: MovieSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieSession = await _context.MovieSessions
                .Include(m => m.CinemaTheaterMovie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieSession == null)
            {
                return NotFound();
            }

            return View(movieSession);
        }

        [Authorize(Roles = "Admin")]
        // GET: MovieSessions/Create
        public IActionResult Create()
        {
            ViewData["CinemaTheaterMovieId"] = new SelectList(_context.CinemaTheaterMovies, "Id", "Id");
            return View();
        }

        // POST: MovieSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CinemaTheaterMovieId,StartedDate,FinishedDate")] MovieSession movieSession)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieSession);
                await _context.SaveChangesAsync();
                return RedirectToAction("MovieSessionList", "Admin");
            }
            ViewData["CinemaTheaterMovieId"] = new SelectList(_context.CinemaTheaterMovies, "Id", "Id", movieSession.CinemaTheaterMovieId);
            return RedirectToAction("MovieSessionList", "Admin");
        }

        [Authorize(Roles = "Admin")]
        // GET: MovieSessions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieSession = await _context.MovieSessions.FindAsync(id);
            if (movieSession == null)
            {
                return NotFound();
            }
            ViewData["CinemaTheaterMovieId"] = new SelectList(_context.CinemaTheaterMovies, "Id", "Id", movieSession.CinemaTheaterMovieId);
            return View(movieSession);
        }

        // POST: MovieSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinemaTheaterMovieId,StartedDate,FinishedDate")] MovieSession movieSession)
        {
            if (id != movieSession.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieSessionExists(movieSession.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MovieSessionList", "Admin");
            }
            ViewData["CinemaTheaterMovieId"] = new SelectList(_context.CinemaTheaterMovies, "Id", "Id", movieSession.CinemaTheaterMovieId);
            return RedirectToAction("MovieSessionList", "Admin");
        }


        [Authorize(Roles = "Admin")]
        // GET: MovieSessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieSession = await _context.MovieSessions
                .Include(m => m.CinemaTheaterMovie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieSession == null)
            {
                return NotFound();
            }

            return View(movieSession);
        }

        // POST: MovieSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieSession = await _context.MovieSessions.FindAsync(id);
            _context.MovieSessions.Remove(movieSession);
            await _context.SaveChangesAsync();
            return RedirectToAction("MovieSessionList", "Admin");
        }

        private bool MovieSessionExists(int id)
        {
            return _context.MovieSessions.Any(e => e.Id == id);
        }
    }
}
