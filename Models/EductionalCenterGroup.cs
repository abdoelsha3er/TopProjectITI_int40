using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class EductionalCenterGroup
    {
        public int EductionalCenterGroupId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Required")]
        [ForeignKey("EductionalCenter")]
        public int EductionalCenterId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [ForeignKey("Grade")]
        public int GradeId { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        //[MaxLength(1000)]
        public int TotleStudents { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int PriceInMonth { get; set; }
        public int Status { get; set; }  // Openning - Closed - Archived
        public string ArchivedReason { get; set; }    // case for archive , may be cancel , may be archived beacuse it's 
        public EductionalCenter EductionalCenter { get; set; }
        public Teacher Teacher { get; set; }
        public Subject Subject { get; set; }
        public Grade Grade { get; set; }
        public List<StudentGroup> StudentsGroups { get; set; }
    }
}
