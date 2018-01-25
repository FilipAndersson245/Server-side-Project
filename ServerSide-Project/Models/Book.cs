using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace ServerSide_Project.Models
{
    public class Book
    {
        [Required]
        [DisplayName("ISBN")]
        [StringLength(13)]
        public string ISBN { get; set; } //PRIMARY KEY

        [Required]
        public string Title { get; set; }

        [Required]
        public int PublicationYear { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        [Required]
        public Author Author{ get; set; }

        [Required]
        public Genre Genre { get; set; }
    }
}