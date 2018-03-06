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
        public List<Book> GetBooksByClassification(int signId)
        {
            ClassificationRepository repo = new ClassificationRepository();
            List<Book> bookList = Mapper.Map<List<BOOK>, List<Book>>(repo.GetBooksFromClassification(signId));
            for (int i = 0; i < bookList.Count; i++)
            {
                //bookList[i] = Book.setupBook(bookList[i]);
            }
            return bookList;
        }

        public Classification GetClassificationFromBookIsbn(string isbn)
        {
            ClassificationRepository repo = new ClassificationRepository();
            return Mapper.Map<Classification>(repo.GetClassificationForBook(isbn));
        }

        public Classification GetClassificationFromID(int id)
        {
            ClassificationRepository repo = new ClassificationRepository();
            return Mapper.Map<CLASSIFICATION, Classification>(repo.GetClassificationFromID(id));
        }

        public bool DeleteClassification(Classification classification)
        {
            ClassificationRepository repo = new ClassificationRepository();
            return repo.DeleteClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public bool CreateClassification(Classification classification)
        {
            ClassificationRepository repo = new ClassificationRepository();
            return repo.CreateClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public List<Classification> GetAllClassifications()
        {
            ClassificationRepository repo = new ClassificationRepository();
            return Mapper.Map<List<CLASSIFICATION>, List<Classification>>(repo.GetAllClassifications());
        }

        public ValidationModel EditClassification(Classification classification)
        {
            ValidationModel validation = new ValidationModel(classification);
            if (validation.IsValid)
            {
                ClassificationRepository repo = new ClassificationRepository();
                if(!repo.EditClassification(Mapper.Map<Classification, CLASSIFICATION>(classification)))
                {
                    validation.DoesNotExistOnServer(nameof(classification.Signum));
                }
            }
            return validation;
        }

    }
}
