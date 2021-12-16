using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class Director
    {
        public int Id { get; set; }

        public string DirectorName { get; set; }

        public string DirectorSurname { get; set; }

        public int? GenderId { get; set; }
        public Gender Gender { get; set; }

        public int? CountryId { get; set; }
        public Country Country { get; set; }

        public DateTime DateOfBirth { get; set; }

    }
}
