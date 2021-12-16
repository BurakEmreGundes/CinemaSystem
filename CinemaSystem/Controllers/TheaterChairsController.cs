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
    public class TheaterChairsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TheaterChairsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TheaterChairs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TheaterChairs.Include(t => t.CinemaTheater);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TheaterChairs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theaterChair = await _context.TheaterChairs
                .Include(t => t.CinemaTheater)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theaterChair == null)
            {
                return NotFound();
            }

            return View(theaterChair);
        }

        // GET: TheaterChairs/Create
        public IActionResult Create()
        {
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "Id");
            return View();
        }

        // POST: TheaterChairs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CinemaTheaterId,ChairCode")] TheaterChair theaterChair)
        {
            if (ModelState.IsValid)
            {
                _context.Add(theaterChair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "Id", theaterChair.CinemaTheaterId);
            return View(theaterChair);
        }

        // GET: TheaterChairs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theaterChair = await _context.TheaterChairs.FindAsync(id);
            if (theaterChair == null)
            {
                return NotFound();
            }
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "Id", theaterChair.CinemaTheaterId);
            return View(theaterChair);
        }

        // POST: TheaterChairs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinemaTheaterId,ChairCode")] TheaterChair theaterChair)
        {
            if (id != theaterChair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(theaterChair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TheaterChairExists(theaterChair.Id))
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
            ViewData["CinemaTheaterId"] = new SelectList(_context.CinemaTheaters, "Id", "Id", theaterChair.CinemaTheaterId);
            return View(theaterChair);
        }

        // GET: TheaterChairs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var theaterChair = await _context.TheaterChairs
                .Include(t => t.CinemaTheater)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (theaterChair == null)
            {
                return NotFound();
            }

            return View(theaterChair);
        }

        // POST: TheaterChairs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var theaterChair = await _context.TheaterChairs.FindAsync(id);
            _context.TheaterChairs.Remove(theaterChair);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TheaterChairExists(int id)
        {
            return _context.TheaterChairs.Any(e => e.Id == id);
        }
    }
}
