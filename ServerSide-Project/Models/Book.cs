using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Helpers;

namespace ServerSide_Project.Models
{
    public class Book
    {
        [Required(ErrorMessage = "ISBN Required")]
        [DisplayName("ISBN")]
        [StringLength(13,ErrorMessage ="Must Be 13 char long")]
        public string ISBN { get; set; } //PRIMARY KEY

        [Required(ErrorMessage = "Must have a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must have a publication year")]
        public int PublicationYear { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        [Required]
        public Author Author{ get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}