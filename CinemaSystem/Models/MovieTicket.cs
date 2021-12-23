using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class MovieTicket
    {
        public int Id { get; set; }

        public int MovieSessionId { get; set; }
        public MovieSession MovieSession { get; set; }

        public DateTime BuyDate { get; set; }

        public double Price { get; set; }

        public int Number { get; set; }

        public string UserId { get; set; }

        public int TheaterChairId { get; set; }
        public TheaterChair TheaterChair { get; set; }
    }
}
