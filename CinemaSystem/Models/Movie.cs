using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string MovieName { get; set; }

        public int? Year { get; set; }

        public int? Time { get; set; }

        public double? IMDB_Puan { get; set; }

        public string Subject { get; set; }

        public string Poster { get; set; }

        public string Fragment { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }


        public int? LanguageId { get; set; }
        public Language Language { get; set; }
        
    }
}
