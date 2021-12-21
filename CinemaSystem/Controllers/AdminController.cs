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

        public async Task<IActionResult> CategoryList()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> CategoryEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> CategoryDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
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

        // GET: Movies/Delete/5
        public async Task<IActionResult> MovieDelete(int? id)
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
            return View(movie);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> CategoryDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
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

        // GET: Actors
        public async Task<IActionResult> ActorList()
        {
            var applicationDbContext = _context.Actors.Include(a => a.ActorRole).Include(a => a.Country).Include(a => a.Gender);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Actors/Create
        public IActionResult ActorCreate()
        {
            ViewData["ActorRoleId"] = new SelectList(_context.ActorRoles, "Id", "RoleType");
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "GenderType");
            return View();
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> ActorEdit(int? id)
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
        // GET: Actors/Delete/5
        public async Task<IActionResult> ActorDelete(int? id)
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

        // GET: Actors/Details/5
        public async Task<IActionResult> ActorDetails(int? id)
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
        // GET: Directors
        public async Task<IActionResult> DirectorList()
        {
            var applicationDbContext = _context.Directors.Include(d => d.Country).Include(d => d.Gender);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: Directors/Details/5
        public async Task<IActionResult> DirectorDetails(int? id)
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
        public IActionResult DirectorCreate()
        {
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName");
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "GenderType");
            return View();
        }

        // GET: Directors/Edit/5
        public async Task<IActionResult> DirectorEdit(int? id)
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
            ViewData["CountryId"] = new SelectList(_context.Countries, "Id", "CountryName", director.CountryId);
            ViewData["GenderId"] = new SelectList(_context.Genders, "Id", "GenderType", director.GenderId);
            return View(director);
        }

        // GET: Directors/Delete/5
        public async Task<IActionResult> DirectorDelete(int? id)
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

        // GET: MovieActors
        public async Task<IActionResult> MovieActorList()
        {
            var applicationDbContext = _context.MovieActors.Include(m => m.Actor).Include(m => m.Movie);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: MovieActors/Details/5
        public async Task<IActionResult> MovieActorDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieActor = await _context.MovieActors
                .Include(m => m.Actor)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieActor == null)
            {
                return NotFound();
            }

            return View(movieActor);
        }

        // GET: MovieActors/Create
        public IActionResult MovieActorCreate()
        {
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "NameSurname");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName");
            return View();
        }

        // GET: MovieActors/Edit/5
        public async Task<IActionResult> MovieActorEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieActor = await _context.MovieActors.FindAsync(id);
            if (movieActor == null)
            {
                return NotFound();
            }
            ViewData["ActorId"] = new SelectList(_context.Actors, "Id", "NameSurname", movieActor.ActorId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName", movieActor.MovieId);
            return View(movieActor);
        }

        // GET: MovieActors/Delete/5
        public async Task<IActionResult> MovieActorDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movieActor = await _context.MovieActors
                .Include(m => m.Actor)
                .Include(m => m.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movieActor == null)
            {
                return NotFound();
            }

            return View(movieActor);
        }


        // GET: MovieDirectors
        public async Task<IActionResult> MovieDirectorList()
        {
            var applicationDbContext = _context.MovieDirectors.Include(m => m.Director).Include(m => m.Movie);
            return View(await applicationDbContext.ToListAsync());
        }
        // GET: MovieDirectors/Details/5
        public async Task<IActionResult> MovieDirectorDetails(int? id)
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
        public IActionResult MovieDirectorCreate()
        {
            ViewData["DirectorId"] = new SelectList(_context.Directors, "Id", "NameSurname");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieName");
            return View();
        }
        // GET: MovieDirectors/Edit/5
        public async Task<IActionResult> MovieDirectorEdit(int? id)
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

        // GET: MovieDirectors/Delete/5
        public async Task<IActionResult> MovieDirectorDelete(int? id)
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


        // GET: ActorRoles
        public async Task<IActionResult> ActorRoleList()
        {
            return View(await _context.ActorRoles.ToListAsync());
        }

        // GET: ActorRoles/Details/5
        public async Task<IActionResult> ActorRoleDetails(int? id)
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
        public IActionResult ActorRoleCreate()
        {
            return View();
        }

        // GET: ActorRoles/Edit/5
        public async Task<IActionResult> ActorRoleEdit(int? id)
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

        // GET: ActorRoles/Delete/5
        public async Task<IActionResult> ActorRoleDelete(int? id)
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

        // GET: Cinemas
        public async Task<IActionResult> CinemaList()
        {
            var applicationDbContext = _context.Cinemas.Include(c => c.District).Include(c => c.Province);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Cinemas/Details/5
        public async Task<IActionResult> CinemaDetails(int? id)
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
        public IActionResult CinemaCreate()
        {
            ViewData["DistrictId"] = new SelectList(_context.Districts, "Id", "DistrictName");
            ViewData["ProvinceId"] = new SelectList(_context.Provinces, "Id", "ProvinceName");
            return View();
        }

        // GET: Cinemas/Edit/5
        public async Task<IActionResult> CinemaEdit(int? id)
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

        // GET: Cinemas/Delete/5
        public async Task<IActionResult> CinemaDelete(int? id)
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


        // GET: CinemaTheaters
        public async Task<IActionResult> CinemaTheaterList()
        {
            var applicationDbContext = _context.CinemaTheaters.Include(c => c.Cinema);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CinemaTheaters/Details/5
        public async Task<IActionResult> CinemaTheaterDetails(int? id)
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

        // GET: CinemaTheaters/Create
        public IActionResult CinemaTheaterCreate()
        {
            ViewData["CinemaId"] = new SelectList(_context.Cinemas, "Id", "CinemaName");
            return View();
        }

        // GET: CinemaTheaters/Edit/5
        public async Task<IActionResult> CinemaTheaterEdit(int? id)
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
        // GET: CinemaTheaters/Delete/5
        public async Task<IActionResult> CinemaTheaterDelete(int? id)
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





    }

}
