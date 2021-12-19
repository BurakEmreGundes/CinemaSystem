using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class CustomerRole:IdentityRole
    {
        public string Description { get; set; }
    }
}
