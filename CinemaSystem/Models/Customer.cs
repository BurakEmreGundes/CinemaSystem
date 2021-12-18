using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class Customer:IdentityUser
    {

        public string Name { get; set; }

        public string Surname { get; set; }

        public DateTime Birthday { get; set; }
    }
}
