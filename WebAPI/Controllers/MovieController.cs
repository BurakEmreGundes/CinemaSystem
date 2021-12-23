using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            _context.Add(movie);
            await _context.SaveChangesAsync();
            return Ok(movie);
        }
    }
}
