using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.ComponentModel.DataAnnotations;

namespace ServerSide_Project.Models
{
    public class Genre
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Genre Requests a name")]
        public string Name { get; set; }

        public Image Art { get; set; }
        
    }
}