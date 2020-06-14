using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "* Required")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }
        //remote call server in background,action >  checkusername in controller "student"
        [Remote("checkusername", "teacher")]  // validation for check username is existed or no.
        [Required(ErrorMessage = "* Required")]
        public string UserName { get; set; }
        [Required]
        [RegularExpression(@"[a-z|A-Z|0-9|.]*@[A-Za-z0-z0-9]+.[a-zA-Z]{2,4}", ErrorMessage = "Email Format Invalid")]
        public string Email { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[*.!@$%^&(){}[]:;<>,.?/~_+-=|\]).{8,32}$", 
        //                ErrorMessage ="Password must contains Special Charachers and Upper Lower case and numbers!.")]
        public string Password { get; set; }
        [StringLength(255)]
        public string Picture { get; set; }

        [Required(ErrorMessage = "* Required")]
        [ForeignKey("City")]
        public int CityId { get; set; }
        [StringLength(255, MinimumLength = 2)]
        public string AddressDetails { get; set; }
        [Required(ErrorMessage = "* Required")]
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [StringLength(500)]
        public string About { get; set; }

        // Navigations
        //public List<TeacherSocialLinks> TeacherLinks { get; set; } // 
        public List<TeacherPhone> TeacherPhones { get; set; }
        public List<TeacherSubjects> TeacherSubjects { get; set; }
        public List<TeacherExperience> TeacherExperiences { get; set; }
        public List<TeacherSocialLink> TeacherSocialLinks { get; set; }
        public List<TeacherSchool> TeacherSchools { get; set; }
        public List<TeacherEduction> TeacherEductions { get; set; }
        public City City { get; set; }
        public List<EductionalCenterGroup> Groups { get; set; }
    }
}

// password pattern
/*
1 - At least one digit [0-9]
2 - At least one lowercase character [a-z]
3 - At least one uppercase character [A-Z]
4 - At least one special character [*.!@#$%^&(){}[]:;<>,.?/~_+-=|\]
5 - At least 8 characters in length, but no more than 32 
*/
