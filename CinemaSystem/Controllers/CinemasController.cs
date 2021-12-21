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
    public class CinemasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CinemasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Cinemas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Cinemas.Include(c => c.District).Include(c => c.Province);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cinemas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinemas
                .Include(c => c.District)
                .Include(c => c.Province)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // GET: Cinemas/Create
        public IActionResult Create()
        {
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "DistrictName");
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "ProvinceName");
            return View();
        }

        // POST: Cinemas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CinemaName,ProvinceId,DistrictId,Address,PhoneNumber,TheaterCount")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cinema);
                await _context.SaveChangesAsync();
                return RedirectToAction("CinemaList", "Admin");
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "DistrictName", cinema.DistrictId);
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "ProvinceName", cinema.ProvinceId);
            return RedirectToAction("CinemaList", "Admin");
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinemas.FindAsync(id);
            if (cinema == null)
            {
                return NotFound();
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "DistrictName", cinema.DistrictId);
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "ProvinceName", cinema.ProvinceId);
            return View(cinema);
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CinemaName,ProvinceId,DistrictId,Address,PhoneNumber,TheaterCount")] Cinema cinema)
        {
            if (id != cinema.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cinema);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CinemaExists(cinema.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("CinemaList", "Admin");
            }
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "DistrictName", cinema.DistrictId);
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "ProvinceName", cinema.ProvinceId);
            return RedirectToAction("CinemaList", "Admin");
        }

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cinema = await _context.Cinemas
                .Include(c => c.District)
                .Include(c => c.Province)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cinema = await _context.Cinemas.FindAsync(id);
            _context.Cinemas.Remove(cinema);
            await _context.SaveChangesAsync();
            return RedirectToAction("CinemaList", "Admin");
        }

        private bool CinemaExists(int id)
        {
            return _context.Cinemas.Any(e => e.Id == id);
        }
    }
}
