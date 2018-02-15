﻿using System;
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
        [StringLength(10, MinimumLength = 10,ErrorMessage ="Must Be 10 char long")] 
        public string ISBN { get; set; } //PRIMARY KEY
         
        [Required(ErrorMessage = "Must have a title")]
        public string Title { get; set; }

        public int PublicationYear { get; set; }

        [MaxLength(1500,ErrorMessage ="Description to long")]
        public string publicationinfo { get; set; }

        //[Required]
        [Range(1,15000, ErrorMessage = "Not valid page number")]
        public short Pages { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage ="Name to long!")]
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

        public static Book setupBook(Book book)
        {
            var author = Mapper.Map<List<AUTHOR>, List<Author>>(EBook.GetAuthorsFromIsbn(book.ISBN)); //get all Authors
            if (author.Count > 0)
            {
                book.Authors = author;
            }
            else
            {
                author.Add(new Author() { FirstName = "No Author", LastName = "Available", BirthYear = 0, Aid = "-1" });
                book.Authors = author;
            }
            return book;
        }

        public static List<Book> setupBooks(List<Book> bookList)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                bookList[i] = setupBook(bookList[i]);
            }
            return bookList;
        }

        public static List<Book> getAllBooks()
        {
            var bookList = Mapper.Map<List<BOOK>,List<Book>>(EBook.getAllBooksFromDB()); //Mapper.Map should convert BOOK to Book (non complex types prob) of type List<>
            return setupBooks(bookList);
        }

        public static List<Book> SearchBooks(string search , params int[] classifications)
        {
            var bookList = Mapper.Map<List<BOOK>, List<Book>>(EBook.GetBookSearchResultat(search,classifications)); // optional send classification and add page index also
            return setupBooks(bookList);
        }

        public static Book getBookFromIsbn(string isbn)
        {
            return Mapper.Map<Book>(EBook.getBookFromIsbn(isbn));
        }

    }
}