using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using CinemaSystem.Data.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System.Threading;
using Microsoft.AspNetCore.Localization;

namespace CinemaSystem.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Customer> _userManager;
        private readonly IStringLocalizer<MoviesController> _localizer;

        public MoviesController(ApplicationDbContext context,IWebHostEnvironment hostEnvironment, UserManager<Customer> userManager, IStringLocalizer<MoviesController> localizer)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _userManager = userManager;
            _localizer = localizer;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Movies.Include(m => m.Category).Include(m => m.Language);
            var movies = await applicationDbContext.ToListAsync();
            ViewBag.movies = movies as ICollection<Movie>;
            return RedirectToAction("MovieList", "Admin");
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id,string culture)
        {
            if (id == null)
            {
                return NotFound();
            }
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture


            if (culture != null)
            {
                var cultureInfo = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
            }
            var selectedCulture = rqf.RequestCulture.Culture;

            ViewData["ctr"] = selectedCulture;
            // LOCALIZATION //
            ViewData["MenuHome"] = _localizer["MenuHome"];
            ViewData["MenuContact"] = _localizer["MenuContact"];
            ViewData["MenuVisions"] = _localizer["MenuVisions"];
            ViewData["ContactUs"] = _localizer["ContactUs"];
            ViewData["MenuProfile"] = _localizer["MenuProfile"];
            ViewData["AddCommentButton"] = _localizer["AddCommentButton"];
            ViewData["BuyTicketButton"] = _localizer["BuyTicketButton"];
            ViewData["MovieNameText"] = _localizer["MovieNameText"];
            ViewData["MovieYearText"] = _localizer["MovieYearText"];
            ViewData["MovieTimeText"] = _localizer["MovieTimeText"];
            ViewData["MovieCategoryText"] = _localizer["MovieCategoryText"];
            ViewData["MovieLanguageText"] = _localizer["MovieLanguageText"];
            ViewData["MovieSubjectText"] = _localizer["MovieSubjectText"];
            // LOCALIZATION //



            var movieComments = (from c in _context.Comments
                                 join m in _context.Movies on c.MovieId equals m.Id
                                 where c.MovieId==id
                                 select new CommentDTO
                                 {
                                     CommentUserId=c.UserId,
                                     CommentTitle = c.Title,
                                     CommentDescription = c.Description
                                 }).ToList();

            foreach (var item in movieComments)
            {
                var user= await _userManager.FindByIdAsync(item.CommentUserId);
            
                    item.UserName = user.Name + " " + user.Surname;
               
               
            }


            var movie = await _context.Movies
                .Include(m => m.Category)
                .Include(m => m.Language)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            ViewBag.movieComments = movieComments;

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieName,Year,Time,IMDB_Puan,Subject,Poster,Fragment,CategoryId,LanguageId")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                /* Resim ekleme kodu */
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\posterimages");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension),FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                };
                movie.Poster = @"\images\posterimages\" + fileName + extension;

                /* Resim ekleme kodu */
                
                _context.Add(movie);
                await _context.SaveChangesAsync();
               return RedirectToAction("MovieCreate", "Admin");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", movie.CategoryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "LanguageName", movie.LanguageId);
             return RedirectToAction("MovieCreate", "Admin");
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", movie.CategoryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id", movie.LanguageId);
            return RedirectToAction("MovieEdit", "Admin");
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieName,Year,Time,IMDB_Puan,Subject,Poster,Fragment,CategoryId,LanguageId")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("MovieList", "Admin");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", movie.CategoryId);
            ViewData["LanguageId"] = new SelectList(_context.Languages, "Id", "Id", movie.LanguageId);
            return RedirectToAction("MovieEdit", "Admin");
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            // return View(movie);
            return RedirectToAction("MovieDelete", "Admin");
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return RedirectToAction("MovieList", "Admin");
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
