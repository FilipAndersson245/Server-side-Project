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

        public static Classification getClassificationFromID(int id)
        {
            return Mapper.Map<CLASSIFICATION, Classification>(ClassificationRepositor.getClassificationFromID(id));
        }

        public static bool deleteClassification(Classification classification)
        {
            return ClassificationRepositor.deleteClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public static bool createClassification(Classification classification)
        {
            return ClassificationRepositor.createClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public static List<Classification> getAllClassifications()
        {
            return Mapper.Map<List<CLASSIFICATION>, List<Classification>>(ClassificationRepositor.GetAllClassifications());
        }

        public static bool editClassification(Classification classification)
        {
            return ClassificationRepositor.editClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

    }
}
