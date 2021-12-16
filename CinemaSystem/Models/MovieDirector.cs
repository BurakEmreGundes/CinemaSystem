using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class MovieDirector
    {
        public int Id { get; set; }

        public int DirectorId { get; set; }
        public int MovieId { get; set; }

        public int? Order { get; set; }


        public Movie Movie { get; set; }
        public Director Director { get; set; }
    }
}
