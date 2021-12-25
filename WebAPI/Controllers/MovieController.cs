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
        private readonly IMovieService _movieService;

        public MovieController(ILogger<MovieController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context=context;
            _movieService = new MovieManager(_context);
     
        }

        [HttpGet]
        public List<Movie> Get()
        {
            return _movieService.GetAll();
        }

        [HttpPost]
        public  IActionResult Post(Movie movie)
        {
            try
            {
                _movieService.Add(movie);
                return Ok(movie);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
            
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            try
            {
                var movie = _movieService.GetById(id);
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
        public IActionResult Delete(int id)
        {

            try
            {
                var movie = _context.Movies.SingleOrDefault(x => x.Id == id);
                if (movie == null)
                {
                    return NotFound();
                }
                _movieService.Delete(id);
                return Ok(movie);

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
         
             
        }

        [HttpPut]
        public  IActionResult Update(int id,Movie movie)
        {
            var entity=_context.Movies.SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            movie.Id = entity.Id;
            _movieService.Update(movie);

            return Ok(entity);
        }
    }
}
