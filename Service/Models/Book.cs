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
using Service.Managers;
using Service.Tools;

namespace Service.Models
{
    public class Book
    {
        public Book()
        {

        }

        //[DisplayName("ISBN")]
        [Required(ErrorMessage = "ISBN Required")]
        [Key]
        [StringLength(10, MinimumLength = 10,ErrorMessage ="Must Be 10 characters long.")] 
        public string ISBN { get; set; } //PRIMARY KEY
         
        [Required(ErrorMessage = "Must have a title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must have a publication year")]
        public int PublicationYear { get; set; }

        [MaxLength(500,ErrorMessage ="Description is too long")]
        public string publicationinfo { get; set; }

        [Required]
        [Range(1,15000, ErrorMessage = "Not a valid page number")]
        public short Pages { get; set; }

        public List<Author> Authors{ get; set; }

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

        public string ShortDescription
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

    }
}