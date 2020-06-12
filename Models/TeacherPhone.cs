using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TopProjectITI_int40.Models
{
    public class TeacherPhone
    {

        public int TeacherPhoneId { get; set; }
        //[Index(IsUnique = true)]    or //public Guid Unique { get; set; } // you can use it for define column in db as a (unique colomn)
        [Required(ErrorMessage = "* Required")]
        public string TeacherPhoneNumber { get; set; }
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }      
        public Teacher Teacher { get; set; }
    }
}


/*
anlother validation
[Validator(typeof(PlaceValidator))]
class Place
{
    public int Id { get; set; }
    public DateTime DateAdded { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}

public class PlaceValidator : AbstractValidator<Place>
{
    public PlaceValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Place Name is required").Length(0, 100);
        RuleFor(x => x.Url).Must(BeUniqueUrl).WithMessage("Url already exists");
    }

    private bool BeUniqueUrl(string url)
    {
        return new DataContext().Places.FirstOrDefault(x => x.Url == url) == null
    }
}
*/
