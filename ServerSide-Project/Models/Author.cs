using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Models
{
    public class Author
    {
        [Required]
        public string ID { get; set; } //Primary Key

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int BirthYear { get; set; }

    }
}