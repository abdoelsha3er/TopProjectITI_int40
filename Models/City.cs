using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class City
    {
        public int CityId { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [ForeignKey("Government")]
        public int GovernmentId { get; set; }
        public Government Government { get; set; }
        public List<Teacher> Teachers { get; set; }
        public List<Parent> Parents { get; set; }
    }
}
