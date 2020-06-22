using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    [Table("StudentsGroups")]
    public class StudentGroup
    {
        [ForeignKey("EductionalCenterGroup")]
        [Key]
        [Column(Order = 0)]
        public int EductionalCenterGroupId { get; set; }
        [ForeignKey("Student")]
        [Key]
        [Column(Order = 1)]
        public int StudentId { get; set; }
        [Required(ErrorMessage =("* Required!!"))]
        public bool IsJoined { get; set; }
        public EductionalCenterGroup EductionalCenterGroup { get; set; }
        public Student Student { get; set; }
    }
}
