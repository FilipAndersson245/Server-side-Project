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

        public IPagedList<Book> BookList { get; set; }

        public static int CreateAuthor(Author author) //Returns Aid if successfull, 0 if failed
        {
            return EAuthor.CreateAuthor(Mapper.Map<Author, AUTHOR>(author));
        }

        public static IPagedList<Author> getAllAuthors(int page, int itemsPerPage)
        {
            return EAuthor.getAllAuthorsFromDB(page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>();
        }

        public static Author getAuthorDetails(int id, int bookPage)
        {
            Author author = Mapper.Map<AUTHOR, Author>(EAuthor.getAuthorDetailsFromDB(id));
            author.BookList = EAuthor.getBooksByAuthor(id, bookPage).ToMappedPagedList<BOOK, Book>();
            Book.setupBooks(author.BookList);
            return author;
        }

        public static IPagedList<Author> getAuthorsFromSearch(string search, int page, int itemsPerPage)
        {
            return EAuthor.getAuthorsFromSearchResult(search, page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>(); ;
        }

    }
}