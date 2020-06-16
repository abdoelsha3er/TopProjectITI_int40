using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        public List<EductionalCenterGroup> Groups { get; set; }
        public List<Student> Students { get; set; }
    }
}
