using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Repository.Support;
using AutoMapper;
using Repository;
using Service.Managers;
using Service.Tools;

namespace Service.Models
{
    public class Classification
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Genre Requests a name")]
        public string Signum { get; set; }

        [Key]
        public int SignId { get; set; }

        public string Description { get; set; }

    }
}