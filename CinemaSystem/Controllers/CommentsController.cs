using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Threading;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Authorization;

namespace CinemaSystem.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;
        private readonly IStringLocalizer<CommentsController> _localizer;

        public CommentsController(ApplicationDbContext context,UserManager<Customer> userManager, IStringLocalizer<CommentsController> localizer)
        {
            _context = context;
            _userManager = userManager;
            _localizer = localizer;
        }


        [Authorize(Roles = "Admin")]
        // GET: Comments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Comments.Include(c => c.Movie);
            return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [Authorize(Roles = "NormalUser")]
        // GET: Comments/Create
        public IActionResult Create(string culture)
        {
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture


            if (culture != null)
            {
                var cultureInfo = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            var selectedCulture = rqf.RequestCulture.Culture;

            ViewData["ctr"] = selectedCulture;
            ViewData["ContactUs"] = _localizer["ContactUs"];
            ViewData["MenuContact"] = _localizer["MenuContact"];
            ViewData["MenuHome"] = _localizer["MenuHome"];
            ViewData["MenuProfile"] = _localizer["MenuProfile"];
            ViewData["MenuVisions"] = _localizer["MenuVisions"];
            ViewData["PageFirstTitle"] = _localizer["PageFirstTitle"];
            ViewData["PagePromotionCode"] = _localizer["PagePromotionCode"];
            ViewData["AddCommentButton"] = _localizer["AddCommentButton"];
            ViewData["InputBoxCommentTitle"] = _localizer["InputBoxCommentTitle"];
            ViewData["InputBoxCommentDesc"] = _localizer["InputBoxCommentDesc"];

            


            ViewBag.MovieId = TempData["MovieId"];
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,UserId,Title,Description")] Comment comment)
        {
            if (ModelState.IsValid)
            {

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Movies", new {Id = comment.MovieId });
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", comment.MovieId);
            return RedirectToAction("Details", "Movies", new { Id = comment.MovieId });
        }

        [Authorize(Roles = "Admin")]
        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", comment.MovieId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,UserId,Title,Description")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", comment.MovieId);
            return View(comment);
        }

        [Authorize(Roles = "Admin")]
        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
