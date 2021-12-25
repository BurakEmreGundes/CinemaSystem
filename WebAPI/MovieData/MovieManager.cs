using CinemaSystem.Data;
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;
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
            _context.Add(movie);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var movieForDelete = _context.Movies.SingleOrDefault(x=>x.Id==id);
            _context.Remove(movieForDelete);
            _context.SaveChanges();
        }

        public List<Movie> GetAll()
        {
            return _context.Movies
                .Include(m=>m.Category)
                .Include(m=>m.Language).ToList();
        }

        public Movie GetById(int id)
        {
            return _context.Movies.Include(m=>m.Category).Include(m=>m.Language).SingleOrDefault(x=>x.Id==id);
        }

        public void Update(Movie movie)
        {
            var entity = _context.Movies.SingleOrDefault(x => x.Id == movie.Id);

            if (movie.MovieName == null)
            {
                movie.MovieName = entity.MovieName;
            }
            if (movie.IMDB_Puan == null)
            {
                movie.IMDB_Puan = entity.IMDB_Puan;
            }
            if (movie.LanguageId == null)
            {
                movie.LanguageId = entity.LanguageId;
            }
            if (movie.CategoryId == null)
            {
                movie.CategoryId = entity.CategoryId;
            }
            if (movie.Poster == null)
            {
                movie.Poster = entity.Poster;
            }
            if (movie.Subject == null)
            {
                movie.Subject = entity.Subject;
            }
            if (movie.Time == null)
            {
                movie.Time = entity.Time;
            }
            if (movie.Year == null)
            {
                movie.Year = entity.Year;
            }
            if (movie.Fragment == null)
            {
                movie.Fragment = entity.Fragment;
            }

            _context.Entry(entity).CurrentValues.SetValues(movie);
            _context.SaveChanges();
        }
    }
}
