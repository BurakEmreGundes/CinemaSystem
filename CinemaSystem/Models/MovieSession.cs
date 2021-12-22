using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{

    // 1 numaralı salonda yer alan filmin seansları burada bulunuyor
    public class MovieSession
    {
        public int Id { get; set; }

        public int CinemaTheaterMovieId { get; set; }
        public CinemaTheaterMovie CinemaTheaterMovie { get; set; }

        public DateTime StartedDate { get; set; } 
        public DateTime FinishedDate { get; set; }
    }
}
