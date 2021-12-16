using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaSystem.Models
{
    public class District
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        public string DistrictName{ get; set; }

        public int ProvinceId { get; set; }
        public Province Province { get; set; }

    }
}
