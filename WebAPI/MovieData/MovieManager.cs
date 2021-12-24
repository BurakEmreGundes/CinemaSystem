using CinemaSystem.Data;
using CinemaSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.MovieData
{
    public class MovieManager : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieManager(ApplicationDbContext context)
        {
            _context = context;
        }
        public void  Add(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var movieForDelete = _context.Movies.SingleOrDefault(x=>x.Id==id);
            _context.Remove(movieForDelete);
        }

        public List<Movie> GetAll()
        {
            return _context.Movies.ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.SingleOrDefault(x=>x.Id==id);
        }

        public void Update(Movie movie)
        {
            
        }
    }
}
