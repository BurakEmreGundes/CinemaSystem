using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class Cinema
    {
        public int Id { get; set; }

        public string CinemaName { get; set; }

        public int? ProvinceId { get; set; }
        public Province Province { get; set; }

        public int? DistrictId { get; set; }
        public District District { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public int TheaterCount { get; set; }
    }
}
