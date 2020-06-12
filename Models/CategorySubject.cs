using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class CategorySubject
    {
        public int CategorySubjectId { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        public List<Subject> Subjects { get; set; }
    }
}
