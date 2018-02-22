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
            List<Book> bookList = Mapper.Map<List<BOOK>,List<Book>>(EClassification.GetBooksFromClassification(signId));
            for (int i = 0; i < bookList.Count; i++)
            {
                //bookList[i] = Book.setupBook(bookList[i]);
            }
            return bookList;
        }

        public static Classification GetClassificationFromBookIsbn(string isbn)
        {
            return Mapper.Map<Classification>(EClassification.GetClassificationForBook(isbn));
        }

        public static Classification getClassificationFromID(int id)
        {
            return Mapper.Map<CLASSIFICATION, Classification>(EClassification.getClassificationFromID(id));
        }

        public static bool deleteClassification(Classification classification)
        {
            return EClassification.deleteClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public static bool createClassification(Classification classification)
        {
            return EClassification.createClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public static List<Classification> getAllClassifications()
        {
            return Mapper.Map<List<CLASSIFICATION>, List<Classification>>(EClassification.GetAllClassifications());
        }

    }
}