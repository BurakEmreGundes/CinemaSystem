using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class Comment
    {

        public int Id { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }

    }
}
