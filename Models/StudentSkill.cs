using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class StudentSkill
    {
        public int StudentSkillId { get; set; }
        public string Description { get; set; }
        [ForeignKey("Student")]
        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
