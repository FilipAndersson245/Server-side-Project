using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Helpers;
using Repository.Support;
using Repository;
using AutoMapper;
using PagedList;

namespace ServerSide_Project.Models
{
    public class Book
    {
        public Book()
        {
            
        }

        //[DisplayName("ISBN")]
        [Required(ErrorMessage = "ISBN Required")]
        [Key]
        [StringLength(11, MinimumLength = 11,ErrorMessage ="Must Be 11 char long")] 
        public string ISBN { get; set; } //PRIMARY KEY
         
        [Required(ErrorMessage = "Must have a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must have a publication year")]
        public int PublicationYear { get; set; }

        [MaxLength(500,ErrorMessage ="Description too long")]
        public string publicationinfo { get; set; }

        [Required]
        [Range(1,15000, ErrorMessage = "Not valid page number")]
        public short Pages { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage ="Name too long!")]
        public List<Author> Authors{ get; set; }

        [Required]
        public Classification BookClassification { get; set; }


        public void SetBook(Book book)
        {
            this.Authors = book.Authors;
            this.BookClassification = book.BookClassification;
            this.Pages = book.Pages;
            this.publicationinfo = book.publicationinfo;
            this.ISBN = book.ISBN;
            this.Title = book.Title;
            this.PublicationYear = book.PublicationYear;
        }

        public string shortDescription
        {
            get
            {
                if(this.publicationinfo == null)
                {
                    return "No description available";
                }
                else if (this.publicationinfo.Length < 255)
                {
                    return this.publicationinfo;
                }
                return this.publicationinfo.Substring(0, 255) + "...";
            }
        }

        public static Book getBookFromIsbn(string isbn)
        {
            var book = Mapper.Map<BOOK,Book>(EBook.getBookFromIsbn(isbn));
            book.Authors = addAuthors(book);
            return book;
        }

        public static List<Author> addAuthors(Book book)
        {
            var authors = Mapper.Map<List<AUTHOR>, List<Author>>(EBook.GetAuthorsFromIsbn(book.ISBN)); //get all Authors
            if (authors.Count > 0)
            {
                return authors;
            }
            else
            {
                authors.Add(new Author() { FirstName = "No Author", LastName = "Available", BirthYear = 0, Aid = "-1" });
                return authors;
            }
        }

        public static void setupBooks(IPagedList<Book> bookList)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                bookList[i].Authors = addAuthors(bookList[i]);
            }
        }

        public static IPagedList<Book> getAllBooks(int page, int itemsPerPage)
        {
            var bookList = EBook.getAllBooksFromDB(page, itemsPerPage).ToMappedPagedList<BOOK, Book>();
            setupBooks(bookList);
            return bookList;
        }

        public static IPagedList<Book> SearchBooks(string search, int page, int itemsPerPage, params int[] classifications)
        {
            var bookList = EBook.GetBookSearchResultat(search, page, itemsPerPage, classifications).ToMappedPagedList<BOOK, Book>();
            setupBooks(bookList);
            return bookList;
        }

    }
}