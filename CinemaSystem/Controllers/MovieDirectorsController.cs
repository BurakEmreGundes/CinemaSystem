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
    public class MovieDirectorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MovieDirectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MovieDirectors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MovieDirectors.Include(m => m.Director).Include(m => m.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MovieDirectors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDirector = await _context.MovieDirectors
                .Include(m => m.Director)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDirector == null)
            {
                return NotFound();
            }

            return View(movieDirector);
        }

        // GET: MovieDirectors/Create
        public IActionResult Create()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "NameSurname");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName");
            return View();
        }

        // POST: MovieDirectors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DirectorId,MovieId,Order")] MovieDirector movieDirector)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movieDirector);
                await _context.SaveChangesAsync();
                return RedirectToAction("MovieDirectorList", "Admin");
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "NameSurname", movieDirector.DirectorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName", movieDirector.MovieId);
            return RedirectToAction("MovieDirectorList", "Admin");
        }

        // GET: MovieDirectors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDirector = await _context.MovieDirectors.FindAsync(id);
            if (movieDirector == null)
            {
                return NotFound();
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "NameSurname", movieDirector.DirectorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName", movieDirector.MovieId);
            return View(movieDirector);
        }

        // POST: MovieDirectors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DirectorId,MovieId,Order")] MovieDirector movieDirector)
        {
            if (id != movieDirector.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movieDirector);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieDirectorExists(movieDirector.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MovieDirectorList", "Admin");
            }
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "NameSurname", movieDirector.DirectorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName", movieDirector.MovieId);
            return RedirectToAction("MovieDirectorList", "Admin");
        }

        // GET: MovieDirectors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieDirector = await _context.MovieDirectors
                .Include(m => m.Director)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieDirector == null)
            {
                return NotFound();
            }

            return View(movieDirector);
        }

        // POST: MovieDirectors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieDirector = await _context.MovieDirectors.FindAsync(id);
            _context.MovieDirectors.Remove(movieDirector);
            await _context.SaveChangesAsync();
            return RedirectToAction("MovieDirectorList", "Admin");
        }

        private bool MovieDirectorExists(int id)
        {
            return _context.MovieDirectors.Any(e => e.Id == id);
        }
    }
}
