using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class CinemaTheater
    {
        public int Id { get; set; }

        public string TheaterNo { get; set; }

        public Cinema Cinema { get; set; }
        public int CinemaId { get; set; }

        public int Capacity { get; set; }
    }
}
