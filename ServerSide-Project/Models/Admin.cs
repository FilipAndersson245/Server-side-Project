using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServerSide_Project.Models
{
    public class Admin
    {
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{7,15}$", ErrorMessage = "password must contain 1 upper and lower case char, one number and be between 7-15 long")]
        public string Password { get; set; }
    }
}