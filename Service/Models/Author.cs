using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository.Support;
using Repository;
using AutoMapper;

namespace ServerSide_Project.Models
{
    public class Author
    {
        public Author()
        {

        }

        [Required]
        [Key]
        [StringLength(10)]
        public string Aid { get; set; } //Primary Key

        [Required]
        [StringLength(50,MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [Required]
        [Range(-2000,2200)]
        public int? BirthYear { get; set; }

        public List<Book> BookList { get; set; }

        public static List<Author> getAllAuthors()
        {
            return Mapper.Map<List<AUTHOR>, List<Author>>(EAuthor.getAllAuthorsFromDB());
        }

        public static Author getAuthorDetails(int id)
        {
            Author author = Mapper.Map<AUTHOR, Author>(EAuthor.getAuthorDetailsFromDB(id));
            author.BookList = Mapper.Map<List<BOOK>, List<Book>>(EAuthor.getBooksByAuthor(id));
            author.BookList = Book.setupBooks(author.BookList);
            return author;
        }

        public static List<Author> getAuthorsFromSearch(string search)
        {
            return Mapper.Map<List<AUTHOR>, List<Author>>(EAuthor.getAuthorsFromSearchResultat(search));
        }

    }
}