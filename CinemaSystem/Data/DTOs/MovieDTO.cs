using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Data.DTOs
{
    public class MovieDTO
    {
        public string MovieName { get; set; }

        public int? Year { get; set; }

        public int? MovieLength { get; set; }

        public string Subject { get; set; }

        public string MovieActors { get; set; }

        public string MovieDirectors { get; set; }

        public DateTime? StartedDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public string Category { get; set; }

        public int MovieID { get; set; }

        public double? IMDB_Puan { get; set; }

        public string Poster { get; set; }

        public string Fragment { get; set; }
    }
}
