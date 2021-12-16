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
    public class CinemaTheaterMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemaTheaterMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CinemaTheaterMovies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CinemaTheaterMovies.Include(c => c.CinemaTheater).Include(c => c.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CinemaTheaterMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTheaterMovie = await _context.CinemaTheaterMovies
                .Include(c => c.CinemaTheater)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemaTheaterMovie == null)
            {
                return NotFound();
            }

            return View(cinemaTheaterMovie);
        }

        // GET: CinemaTheaterMovies/Create
        public IActionResult Create()
        {
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: CinemaTheaterMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CinemaTheaterId,MovieId")] CinemaTheaterMovie cinemaTheaterMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinemaTheaterMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "Id", cinemaTheaterMovie.CinemaTheaterId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", cinemaTheaterMovie.MovieId);
            return View(cinemaTheaterMovie);
        }

        // GET: CinemaTheaterMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTheaterMovie = await _context.CinemaTheaterMovies.FindAsync(id);
            if (cinemaTheaterMovie == null)
            {
                return NotFound();
            }
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "Id", cinemaTheaterMovie.CinemaTheaterId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", cinemaTheaterMovie.MovieId);
            return View(cinemaTheaterMovie);
        }

        // POST: CinemaTheaterMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinemaTheaterId,MovieId")] CinemaTheaterMovie cinemaTheaterMovie)
        {
            if (id != cinemaTheaterMovie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinemaTheaterMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaTheaterMovieExists(cinemaTheaterMovie.Id))
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
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "Id", cinemaTheaterMovie.CinemaTheaterId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", cinemaTheaterMovie.MovieId);
            return View(cinemaTheaterMovie);
        }

        // GET: CinemaTheaterMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTheaterMovie = await _context.CinemaTheaterMovies
                .Include(c => c.CinemaTheater)
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemaTheaterMovie == null)
            {
                return NotFound();
            }

            return View(cinemaTheaterMovie);
        }

        // POST: CinemaTheaterMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaTheaterMovie = await _context.CinemaTheaterMovies.FindAsync(id);
            _context.CinemaTheaterMovies.Remove(cinemaTheaterMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CinemaTheaterMovieExists(int id)
        {
            return _context.CinemaTheaterMovies.Any(e => e.Id == id);
        }
    }
}
