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
    public class CinemaTheatersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemaTheatersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: CinemaTheaters
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CinemaTheaters.Include(c => c.Cinema);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: CinemaTheaters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTheater = await _context.CinemaTheaters
                .Include(c => c.Cinema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemaTheater == null)
            {
                return NotFound();
            }

            return View(cinemaTheater);
        }

        [Authorize(Roles = "Admin")]
        // GET: CinemaTheaters/Create
        public IActionResult Create()
        {
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "CinemaName");
            return View();
        }

        // POST: CinemaTheaters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TheaterNo,CinemaId,Capacity")] CinemaTheater cinemaTheater)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinemaTheater);
                await _context.SaveChangesAsync();
                return RedirectToAction("CinemaTheaterList", "Admin");
            }
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "CinemaName", cinemaTheater.CinemaId);
            return RedirectToAction("CinemaTheaterList", "Admin");
        }

        [Authorize(Roles = "Admin")]
        // GET: CinemaTheaters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTheater = await _context.CinemaTheaters.FindAsync(id);
            if (cinemaTheater == null)
            {
                return NotFound();
            }
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "CinemaName", cinemaTheater.CinemaId);
            return View(cinemaTheater);
        }

        // POST: CinemaTheaters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TheaterNo,CinemaId,Capacity")] CinemaTheater cinemaTheater)
        {
            if (id != cinemaTheater.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinemaTheater);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaTheaterExists(cinemaTheater.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CinemaTheaterList", "Admin");
            }
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "CinemaName", cinemaTheater.CinemaId);
            return RedirectToAction("CinemaTheaterList", "Admin");
        }

        [Authorize(Roles = "Admin")]
        // GET: CinemaTheaters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinemaTheater = await _context.CinemaTheaters
                .Include(c => c.Cinema)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinemaTheater == null)
            {
                return NotFound();
            }

            return View(cinemaTheater);
        }

        // POST: CinemaTheaters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinemaTheater = await _context.CinemaTheaters.FindAsync(id);
            _context.CinemaTheaters.Remove(cinemaTheater);
            await _context.SaveChangesAsync();
            return RedirectToAction("CinemaTheaterList", "Admin");
        }

        private bool CinemaTheaterExists(int id)
        {
            return _context.CinemaTheaters.Any(e => e.Id == id);
        }
    }
}
