using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Repository.Support;
using Repository;
using AutoMapper;
using PagedList;
using Service.Managers;
using Service.Tools;

namespace Service.Models
{
    public class BookAuthorClassification
    {

        public Book Book { get; set; }

        [Required]
        public List<Author> Authors { get; set; }

        [Required]
        public List<Classification> Classifications { get; set; }
    }
}
