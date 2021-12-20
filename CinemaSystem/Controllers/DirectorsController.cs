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
    public class DirectorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DirectorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Directors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Directors.Include(d => d.Country).Include(d => d.Gender);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Directors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .Include(d => d.Country)
                .Include(d => d.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // GET: Directors/Create
        public IActionResult Create()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id");
            return View();
        }

        // POST: Directors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DirectorName,DirectorSurname,GenderId,CountryId,DateOfBirth")] Director director)
        {
            if (ModelState.IsValid)
            {
                _context.Add(director);
                await _context.SaveChangesAsync();
                return RedirectToAction("DirectorList", "Admin");
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", director.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", director.GenderId);
            return RedirectToAction("DirectorList", "Admin");
        }

        // GET: Directors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", director.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", director.GenderId);
            return View(director);
        }

        // POST: Directors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DirectorName,DirectorSurname,GenderId,CountryId,DateOfBirth")] Director director)
        {
            if (id != director.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(director);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DirectorExists(director.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("DirectorList", "Admin");
            }
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "Id", director.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "Id", director.GenderId);
            return RedirectToAction("DirectorList", "Admin");
        }

        // GET: Directors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var director = await _context.Directors
                .Include(d => d.Country)
                .Include(d => d.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (director == null)
            {
                return NotFound();
            }

            return View(director);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var director = await _context.Directors.FindAsync(id);
            _context.Directors.Remove(director);
            await _context.SaveChangesAsync();
            return RedirectToAction("DirectorList", "Admin");
        }

        private bool DirectorExists(int id)
        {
            return _context.Directors.Any(e => e.Id == id);
        }
    }
}
