using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class MovieSession
    {
        public int Id { get; set; }

        public int CinemaTheaterMovieId { get; set; }
        public CinemaTheaterMovie CinemaTheaterMovie { get; set; }

        public DateTime StartedDate { get; set; } 
        public DateTime FinishedDate { get; set; }
    }
}
