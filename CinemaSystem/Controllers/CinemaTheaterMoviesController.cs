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
    public class CinemaTheaterMoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemaTheaterMoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: CinemaTheaterMovies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CinemaTheaterMovies.Include(c => c.CinemaTheater).Include(c => c.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        // GET: CinemaTheaterMovies/Create
        public IActionResult Create()
        {
    
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "TheaterNo");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName");
            return View();
        }

        // POST: CinemaTheaterMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CinemaTheaterId,MovieId,StartedDate,FinishedDate,Subtitle")] CinemaTheaterMovie cinemaTheaterMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinemaTheaterMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction("CinemaTheaterMovieList", "Admin");
            }
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "TheaterNo", cinemaTheaterMovie.CinemaTheaterId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName", cinemaTheaterMovie.MovieId);
            return RedirectToAction("CinemaTheaterMovieList", "Admin");
        }

        [Authorize(Roles = "Admin")]
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
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "TheaterNo", cinemaTheaterMovie.CinemaTheaterId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName", cinemaTheaterMovie.MovieId);
            return View(cinemaTheaterMovie);
        }

        // POST: CinemaTheaterMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinemaTheaterId,MovieId,StartedDate,FinishedDate,Subtitle")] CinemaTheaterMovie cinemaTheaterMovie)
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
                return RedirectToAction("CinemaTheaterMovieList", "Admin");
            }
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "TheaterNo", cinemaTheaterMovie.CinemaTheaterId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName", cinemaTheaterMovie.MovieId);
            return RedirectToAction("CinemaTheaterMovieList", "Admin");
        }

        [Authorize(Roles = "Admin")]
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
            return RedirectToAction("CinemaTheaterMovieList", "Admin");
        }

        private bool CinemaTheaterMovieExists(int id)
        {
            return _context.CinemaTheaterMovies.Any(e => e.Id == id);
        }
    }
}
