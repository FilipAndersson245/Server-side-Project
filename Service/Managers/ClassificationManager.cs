using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Repository.Support;
using AutoMapper;
using Repository;
using Service.Models;
using Service.Tools;

namespace Service.Managers
{
    public class ClassificationManager
    {
        public static List<Book> GetBooksByClassification(int signId)
        {
            List<Book> bookList = Mapper.Map<List<BOOK>, List<Book>>(ClassificationRepositor.GetBooksFromClassification(signId));
            for (int i = 0; i < bookList.Count; i++)
            {
                //bookList[i] = Book.setupBook(bookList[i]);
            }
            return bookList;
        }

        public static Classification GetClassificationFromBookIsbn(string isbn)
        {
            return Mapper.Map<Classification>(ClassificationRepositor.GetClassificationForBook(isbn));
        }

        public static Classification GetClassificationFromID(int id)
        {
            return Mapper.Map<CLASSIFICATION, Classification>(ClassificationRepositor.GetClassificationFromID(id));
        }

        public static bool DeleteClassification(Classification classification)
        {
            return ClassificationRepositor.DeleteClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public static bool CreateClassification(Classification classification)
        {
            return ClassificationRepositor.CreateClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public static List<Classification> GetAllClassifications()
        {
            return Mapper.Map<List<CLASSIFICATION>, List<Classification>>(ClassificationRepositor.GetAllClassifications());
        }

        public static bool EditClassification(Classification classification)
        {
            return ClassificationRepositor.EditClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

    }
}
