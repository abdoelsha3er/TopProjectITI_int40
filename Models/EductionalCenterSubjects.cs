using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class EductionalCenterSubjects
    {
        [ForeignKey("Subject")]
        [Key]
        [Column(Order = 0)]
        public int SubjectId { get; set; }
        [ForeignKey("Teacher")]
        [Key]
        [Column(Order = 1)]
        public int EductionalCenterId { get; set; }
        public Subject Subject { get; set; }
        public EductionalCenter EductionalCenter { get; set; }
    }
}
