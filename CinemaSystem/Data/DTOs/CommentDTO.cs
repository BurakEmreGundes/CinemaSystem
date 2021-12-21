using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Data.DTOs
{
    public class CommentDTO
    {
        public string CommentTitle { get; set; }
        public string CommentDescription { get; set; }
        public string UserName { get; set; }
        public string CommentUserId { get; set; }
    }
}
