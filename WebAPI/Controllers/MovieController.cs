using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.MovieData;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController : ControllerBase
    {
        
        private readonly ILogger<MovieController> _logger;
        private readonly ApplicationDbContext _context;


        public MovieController(ILogger<MovieController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context=context;
     
        }

        [HttpGet]
        public List<Movie> Get()
        {
            return _context.Movies.ToList();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Movie movie)
        {
            try
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return Ok(movie);
            }
            catch (Exception e)
            {

                return BadRequest();
            }
            
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            try
            {
                var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
                if (movie == null)
                {
                    return NotFound();
                }
                return Ok(movie);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
          
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var movieForDelete = _context.Movies.SingleOrDefault(x => x.Id == id);
                if (movieForDelete == null)
                {
                    return NotFound();
                }
                _context.Movies.Remove(movieForDelete);
                await _context.SaveChangesAsync();
                return Ok(movieForDelete);

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
             
        }

        [HttpPut]
        public  async Task<IActionResult> Update(int id,Movie movie)
        {
            var entity=_context.Movies.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                
                return NotFound();
            }
            movie.Id = entity.Id;
            movie.MovieName = movie.MovieName!=null ? movie.MovieName : entity.MovieName;
            movie.IMDB_Puan = movie.IMDB_Puan != null ? movie.IMDB_Puan : entity.IMDB_Puan;
            movie.LanguageId= movie.LanguageId != null ? movie.LanguageId : entity.LanguageId;
            movie.Poster = movie.Poster != null ? movie.Poster : entity.Poster;
            movie.Subject = movie.Subject != null ? movie.Subject : entity.Subject;
            movie.Time= movie.Time != null ? movie.Time : entity.Time;
            movie.Year = movie.Year != null ? movie.Year : entity.Year;
            movie.Fragment = movie.Fragment != null ? movie.Fragment : entity.Fragment;
            movie.CategoryId = movie.CategoryId != null ? movie.CategoryId : entity.CategoryId;


            _context.Entry(entity).CurrentValues.SetValues(movie);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }
    }
}
