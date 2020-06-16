using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class EductionalCenter
    {
        public int EductionalCenterId { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9]*@[A-Za-z0-z0-9]+.[a-zA-Z]{2,4}", ErrorMessage = "Email Format Invalid")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Picture { get; set; }
        public string About { get; set; }
        [Required(ErrorMessage = "* Required")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string AddressDetails { get; set; }
        public List<EductionalCenterPhone> EductionalCenterPhones { get; set; }
        public List<EductionalCenterSubjects> EductionalCenterSubjects { get; set; }
        public List<EductionalCenterSoicalLink> EductionalCenterSoicalLinks { get; set; }
        public List<EductionalCenterGroup> EductionalCenterGroups { get; set; }
    }
}
