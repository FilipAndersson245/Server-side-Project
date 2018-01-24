using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Models
{
    public class Book
    {

        public string ISBN { get; set; }

        public string Title { get; set; }

        public int PublicationYear { get; set; }

        public string Description { get; set; }

        public int Pages { get; set; }

        public Author Author{ get; set; }

        public Genre Genre { get; set; }
    }
}