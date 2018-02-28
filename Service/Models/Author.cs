using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
    public class Author
    {
        public Author(){}

        [Required]
        [Key]
        [StringLength(10)]
        public string Aid { get; set; } //Primary Key

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [Range(-2000, 2200)]
        public int? BirthYear { get; set; }

        public IPagedList<Book> BookList { get; set; }

        public string FullName
        {
            get
            {
                return this.LastName + " " + this.FirstName;
            }
        }

    }
}