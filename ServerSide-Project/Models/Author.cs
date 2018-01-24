using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ServerSide_Project.Models
{
    public class Author
    {
        public string ID { get; set; } //Primary Key

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int BirthYear { get; set; }

    }
}