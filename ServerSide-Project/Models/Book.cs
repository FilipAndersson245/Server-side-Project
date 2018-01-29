﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Helpers;

namespace ServerSide_Project.Models
{
    public class Book
    {
        [DisplayName("ISBN")]
        [Required(ErrorMessage = "ISBN Required")]
        //[StringLength(13, MinimumLength = 13,ErrorMessage ="Must Be 13 char long")] not implamented as of testing
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
    }
}