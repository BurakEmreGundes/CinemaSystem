using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class TheaterChair
    {
        public int Id { get; set; }

        public int CinemaTheaterId { get; set; }
        public CinemaTheater CinemaTheater { get; set; }
        
        public string ChairCode { get; set; }
    }
}
