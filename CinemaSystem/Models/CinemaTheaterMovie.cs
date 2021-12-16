using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{

    // Bu sınıfta sinema filmlerinin hangi salonda ne kadar vizyonda olacağı yer alıyor
    public class CinemaTheaterMovie
    {
        public int Id { get; set; }

        public int? CinemaTheaterId { get; set; }
        public CinemaTheater CinemaTheater { get; set; }

        public int? MovieId { get; set; }
        public Movie Movie { get; set; }

        [DataType(DataType.Date)]
        public DateTime StartedDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime FinishedDate { get; set; }

        public bool Subtitle { get; set; }
    }
}
