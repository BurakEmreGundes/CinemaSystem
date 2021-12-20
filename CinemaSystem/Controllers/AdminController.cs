using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {

        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CategoryCreate()
        {
            return View();
        }

        public IActionResult MovieCreate()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName");
            return View();
        }

        public async Task<IActionResult> MovieList()
        {
            var applicationDbContext = _context.Movies.Include(m => m.Category).Include(m => m.Language);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> MovieEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", movie.CategoryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName", movie.LanguageId);
            return View(movie);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> MovieDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }
    }
}
