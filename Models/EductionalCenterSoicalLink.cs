using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class EductionalCenterSoicalLink
    {
        public int EductionalCenterSoicalLinkId { get; set; }
        [Required(ErrorMessage = "*")]
        public string LinkAddess { get; set; }
        [ForeignKey("EductionalCenter")]
        public int EductionalCenterId { get; set; }
        public EductionalCenter EductionalCenter { get; set; }
    }
}
