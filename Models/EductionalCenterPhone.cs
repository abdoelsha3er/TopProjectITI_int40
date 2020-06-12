using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class EductionalCenterPhone
    {
        public int EductionalCenterPhoneId { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string EductionalCenterPhoneNumber { get; set; }
        [ForeignKey("EductionalCenter")]
        public int EductionalCenterId { get; set; }
        public EductionalCenter EductionalCenter { get; set; }




    }
}
