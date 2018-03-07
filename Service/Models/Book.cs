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

        public string ISBN { get; set; } //PRIMARY KEY
         
        public string Title { get; set; }

        public int PublicationYear { get; set; }

        public string Publicationinfo { get; set; }

        public short Pages { get; set; }

        public List<Author> Authors{ get; set; }

        public Classification Classification { get; set; }

        public int SignId { get; set; }

        public void SetBook(Book book)
        {
            this.Authors = book.Authors;
            this.Classification = book.Classification;
            this.Pages = book.Pages;
            this.Publicationinfo = book.Publicationinfo;
            this.ISBN = book.ISBN;
            this.Title = book.Title;
            this.PublicationYear = book.PublicationYear;
        }

        public string ShortDescription
        {
            get
            {
                if(this.Publicationinfo == null)
                {
                    return "No description available";
                }
                else if (this.Publicationinfo.Length < 255)
                {
                    return this.Publicationinfo;
                }
                return this.Publicationinfo.Substring(0, 255) + "...";
            }
        }

    }
}