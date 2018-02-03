using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Repository.Support;

namespace ServerSide_Project.Models
{
    public class Genre
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Genre Requests a name")]
        public string Name { get; set; }

        [Key]
        public String Signid { get; set; }
        
    }
}