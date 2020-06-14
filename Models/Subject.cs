using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "*")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [ForeignKey("CategorySubject")]
        public int CategorySubjectId { get; set; }
        public CategorySubject CategorySubject { get; set; }
        public List<TeacherSubjects> TeacherSubjects { get; set; }
        public List<EductionalCenterSubjects> EductionalCenterSubjects { get; set; }
        public List<EductionalCenterGroup> Groups { get; set; }
    }
}
