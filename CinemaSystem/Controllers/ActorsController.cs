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
    
    public class ActorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Actors.Include(a => a.ActorRole).Include(a => a.Country).Include(a => a.Gender);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Actors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .Include(a => a.ActorRole)
                .Include(a => a.Country)
                .Include(a => a.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        [Authorize]
        // GET: Actors/Create
        public IActionResult Create()
        {
            ViewData["ActorRoleId"] = new SelectList(_context.ActorRoles, "Id", "RoleType");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "GenderType");
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ActorName,ActorSurname,CountryId,GenderId,DateOfBirth,ActorRoleId")] Actor actor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actor);
                await _context.SaveChangesAsync();
                return RedirectToAction("ActorList", "Admin");
            }
            ViewData["ActorRoleId"] = new SelectList(_context.ActorRoles, "Id", "RoleType", actor.ActorRoleId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", actor.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "GenderType", actor.GenderId);
            return RedirectToAction("ActorList", "Admin");
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors.FindAsync(id);
            if (actor == null)
            {
                return NotFound();
            }
            ViewData["ActorRoleId"] = new SelectList(_context.ActorRoles, "Id", "RoleType", actor.ActorRoleId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", actor.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "GenderType", actor.GenderId);
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ActorName,ActorSurname,CountryId,GenderId,DateOfBirth,ActorRoleId")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorExists(actor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ActorList", "Admin");
            }
            ViewData["ActorRoleId"] = new SelectList(_context.ActorRoles, "Id", "RoleType", actor.ActorRoleId);
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", actor.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "GenderType", actor.GenderId);
            return RedirectToAction("ActorList", "Admin");
        }

        // GET: Actors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actor = await _context.Actors
                .Include(a => a.ActorRole)
                .Include(a => a.Country)
                .Include(a => a.Gender)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actor == null)
            {
                return NotFound();
            }

            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actor = await _context.Actors.FindAsync(id);
            _context.Actors.Remove(actor);
            await _context.SaveChangesAsync();
            return RedirectToAction("ActorList", "Admin");
        }

        private bool ActorExists(int id)
        {
            return _context.Actors.Any(e => e.Id == id);
        }
    }
}
