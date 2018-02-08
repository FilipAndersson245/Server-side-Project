using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Repository.Support;
using AutoMapper;
using Repository;

namespace ServerSide_Project.Models
{
    public class Classification
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Genre Requests a name")]
        public string Signum { get; set; }

        [Key]
        public int SignId { get; set; }

        public string Description { get; set; }

        public static List<Book> GetBooksByClassification(int signId)
        {
            return Mapper.Map<List<BOOK>,List<Book>>(EClassification.GetBooksFromClassification(signId));
        }

        public static Classification GetClassificationFromBookIsbn(string isbn)
        {
            return Mapper.Map<Classification>(EClassification.GetClassificationForBook(isbn));
        }

    }
}