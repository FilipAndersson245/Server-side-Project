using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide_Project.Models
{
    public class AuthorAndGenre
    {
        public List<Author> AuthorList { get; set;}
        public List<Genre> GenreList {get; set;}

        public Book book { get; set;}
    }
}