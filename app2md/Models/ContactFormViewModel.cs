using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace app2md.Models
{
    public class ContactFormViewModel
    {
        [Required]
        [DisplayName("First Name")]
        [Remote("ValidateFirstName", "Main", ErrorMessage = "The first name must not have banned letters of: x,v")]

        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [Remote("ValidateLastName", "Main", ErrorMessage = "The last name must not have banned letters of: x,v")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Area Of Interests")]
        public string AreaOfInterests { get; set; }

        [Required]
        public string Message { get; set; }

    }
}
