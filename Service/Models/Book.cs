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

namespace ServerSide_Project.Models
{
    public class Book
    {
        //[DisplayName("ISBN")]
        [Required(ErrorMessage = "ISBN Required")]
        [Key]
        [StringLength(11, MinimumLength = 11,ErrorMessage ="Must Be 11 char long")] 
        public string ISBN { get; set; } //PRIMARY KEY

        [Required(ErrorMessage = "Must have a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must have a publication year")]
        public int PublicationYear { get; set; }

        [MaxLength(500,ErrorMessage ="Description to long")]
        public string Description { get; set; }

        [Required]
        [Range(1,15000, ErrorMessage = "Not valid page number")]
        public int Pages { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage ="Name to long!")]
        public Author BookAuthor{ get; set; }

        [Required]
        public Genre BookGenre { get; set; }


        public void SetBook(Book book)
        {
            this.BookAuthor = book.BookAuthor;
            this.BookGenre = book.BookGenre;
            this.Pages = book.Pages;
            this.Description = book.Description;
            this.ISBN = book.ISBN;
            this.Title = book.Title;
            this.PublicationYear = book.PublicationYear;
        }

        public string ShortDescription
        {
            get
            {
                if (this.Description == null)
                {
                    return "No Description Available.";
                }
                else if (this.Description.Length < 550)
                {
                    return this.Description;
                }
                return this.Description.Substring(0, 550) + "...";
            }
        }

        public static List<Book> getAllBooks()
        {
            var abc = new List<Book>();
            var eBookList = EBook.getAllBooksFromDB();

            foreach(var book in eBookList)
            {
                var a = convertBOOKtoBook(book);
                abc.Add(a);
                if(abc.Count > 1000)
                {
                    return abc;
                }
            }
            return abc;
        }

        private static Book convertBOOKtoBook(BOOK dbBook)
        {
            var authorList = EBook.GetAuthorsFromIsbn(dbBook.ISBN);
            return new Book()
            {
                ISBN = dbBook.ISBN,
                Title = dbBook.Title,
                PublicationYear = Convert.ToInt32(dbBook.PublicationYear),
                Description = dbBook.publicationinfo,
                Pages = Convert.ToInt32(dbBook.pages),
                BookAuthor = new Author() { ID = authorList[0].Aid.ToString(), FirstName = authorList[0].FirstName, LastName = authorList[0].LastName, BirthYear = Convert.ToInt32(authorList[0].BirthYear) }

            };
        }

    }
}