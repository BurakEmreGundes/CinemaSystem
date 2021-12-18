using CinemaSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CinemaSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<Customer>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Language> Languages { get; set; }

        public DbSet<Movie> Movies { get; set; }

        public DbSet<Actor> Actors { get; set; }

        public DbSet<MovieActor> MovieActors { get; set; }

        public DbSet<ActorRole> ActorRoles { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<CinemaTheater> CinemaTheaters { get; set; }

        public DbSet<CinemaTheaterMovie> CinemaTheaterMovies { get; set; }

        public DbSet<Cinema> Cinemas { get; set; }

        public DbSet<Director> Directors { get; set; }

        public DbSet<MovieDirector> MovieDirectors { get; set; }

        public DbSet<TheaterChair> TheaterChairs { get; set; }

        public DbSet<MovieTicket> MovieTickets { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<MovieSession> MovieSessions { get; set; }





    }
}