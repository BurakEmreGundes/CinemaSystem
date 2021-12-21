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
    public class ActorRolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActorRolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ActorRoles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ActorRoles.ToListAsync());
        }

        // GET: ActorRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRole = await _context.ActorRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorRole == null)
            {
                return NotFound();
            }

            return View(actorRole);
        }

        // GET: ActorRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ActorRoles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoleType")] ActorRole actorRole)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actorRole);
                await _context.SaveChangesAsync();
                return RedirectToAction("ActorRoleList", "Admin");
            }
            return RedirectToAction("ActorRoleList", "Admin");
        }

        // GET: ActorRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRole = await _context.ActorRoles.FindAsync(id);
            if (actorRole == null)
            {
                return NotFound();
            }
            return View(actorRole);
        }

        // POST: ActorRoles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoleType")] ActorRole actorRole)
        {
            if (id != actorRole.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actorRole);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActorRoleExists(actorRole.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("ActorRoleList", "Admin");
            }
            return RedirectToAction("ActorRoleList", "Admin");
        }

        // GET: ActorRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actorRole = await _context.ActorRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (actorRole == null)
            {
                return NotFound();
            }

            return View(actorRole);
        }

        // POST: ActorRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorRole = await _context.ActorRoles.FindAsync(id);
            _context.ActorRoles.Remove(actorRole);
            await _context.SaveChangesAsync();
            return RedirectToAction("ActorRoleList", "Admin");
        }

        private bool ActorRoleExists(int id)
        {
            return _context.ActorRoles.Any(e => e.Id == id);
        }
    }
}
