
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DDACAssignment.Models
{
    public class register
    {
        [Required]
        [Display(Name = "Full Name")]
        //[StringLength(256, ErrorMessage = "The length should be between 10 to 256 characters", MinimumLength = 10)]
        [DataType(DataType.Text)]
        public string Name { get; set; }


        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail not valid")]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }

        [Required]
        public String Role { get; set; }


    }
}
