using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class CinemaTheaterMovie
    {
        public int Id { get; set; }

        public int? CinemaTheaterId { get; set; }
        public CinemaTheater CinemaTheater { get; set; }

        public int? MovieId { get; set; }
        public Movie Movie { get; set; }


        public bool Subtitle = true;
    }
}
