using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class Parent
    {
        public int ParentId { get; set; }
        [Required(ErrorMessage = "* Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string LastName { get; set; }
        [Remote("checkUserName", "parent")]  // validation for check username is existed or no.
        public string UserName { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$")]
        public string Password { get; set; }
        [Required]
        [RegularExpression(@"[a-zA-Z0-9]*@[A-Za-z0-z0-9]+.[a-zA-Z]{2,4}", ErrorMessage = "Email Format Invalid")]
        public string Email { get; set; }
        [Required]
        [StringLength(11)]
        public string Phone { get; set; }
        public string Picture { get; set; }
        [Required]
        [ForeignKey("City")]
        public int CityId { get; set; }

        public City City { get; set; }

        public string Street { get; set; }
    }
}
