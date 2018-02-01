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
        public Author()
        {

        }

        public Author(string id, Repository repo)
        {
            ID = id;
            BookList = repo.BookList.FindAll(x => (x.BookAuthor.ID == id));
            FirstName = repo.AuthorList.Find(x => (x.ID == id)).FirstName;
            LastName = repo.AuthorList.Find(x => (x.ID == id)).LastName;
            BirthYear = repo.AuthorList.Find(x => (x.ID == id)).BirthYear;
        }

        [Required]
        [Key]
        [StringLength(10)]
        public string ID { get; set; } //Primary Key

        [Required]
        [StringLength(50,MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [Range(-2000,2200)]
        public int BirthYear { get; set; }

        [Required]
        public List<Book> BookList { get; set; }
    }
}