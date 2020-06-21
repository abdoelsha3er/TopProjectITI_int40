using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "*  Required")]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$")]
        public string Password { get; set; }
        [Required(ErrorMessage = "* Required")]
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string School { get; set; }
        public int DegreeOfLastYear { get; set; }
        public string Picture { get; set; }
        [Required(ErrorMessage = "* Required")]
        [ForeignKey("Parent")]
        public int ParentId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [ForeignKey("Grade")]
        public int GradeId { get; set; }
        public Parent Parent { get; set; }
        public Grade Grade { get; set; }
        public List<StudentGroup> StudentsGroups { get; set; }
        public List<StudentSkill> StudentSkills { get; set; }
    }
}
