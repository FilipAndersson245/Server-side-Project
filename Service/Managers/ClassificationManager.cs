using AutoMapper;
using Repository;
using Repository.Support;
using Service.Models;
using Service.Validations;
using System.Collections.Generic;

namespace Service.Managers
{
    public class ClassificationManager
    {
        public List<Book> GetBooksByClassification(int signId)
        {
            ClassificationRepository repo = new ClassificationRepository();
            List<Book> bookList = Mapper.Map<List<BOOK>, List<Book>>(repo.GetBooksFromClassification(signId));
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

        public Classification GetClassificationFromName(string name)
        {
            ClassificationRepository repo = new ClassificationRepository();
            return Mapper.Map<CLASSIFICATION, Classification>(repo.GetClassificationFromName(name));
        }

        public Classification AddGenericClassification()
        {
            ClassificationRepository classRepo = new ClassificationRepository();
            if (classRepo.DoesClassificationExist("Generic"))
            {
                return Mapper.Map<CLASSIFICATION, Classification>(classRepo.GetClassificationFromName("Generic"));
            }
            else
            {
                Classification genericClass = new Classification() { Signum = "Generic", Description = "Books without a category" };
                if (classRepo.CreateClassification(Mapper.Map<Classification, CLASSIFICATION>(genericClass)))
                {
                    return genericClass;
                }
                else
                {
                    return genericClass; //Add some warning for user maybe
                }
            }
        }

        public bool DeleteClassification(Classification classification)
        {
            ClassificationRepository repo = new ClassificationRepository();
            return repo.DeleteClassification(Mapper.Map<Classification, CLASSIFICATION>(classification));
        }

        public ClassificationValidation CreateClassification(Classification classification)
        {
            ClassificationValidation validation = new ClassificationValidation(classification);
            if (validation.IsValid)
            {
                ClassificationRepository repo = new ClassificationRepository();
                if (!repo.CreateClassification(Mapper.Map<Classification, CLASSIFICATION>(classification)))
                {
                    validation.FailedToCreateClassification(nameof(classification.Signum));
                }
            }
            return validation;
        }

        public List<Classification> GetAllClassifications()
        {
            ClassificationRepository repo = new ClassificationRepository();
            return Mapper.Map<List<CLASSIFICATION>, List<Classification>>(repo.GetAllClassifications());
        }

        public ClassificationValidation EditClassification(Classification classification)
        {
            ClassificationValidation validation = new ClassificationValidation(classification);
            if (validation.IsValid)
            {
                ClassificationRepository repo = new ClassificationRepository();
                if (!repo.EditClassification(Mapper.Map<Classification, CLASSIFICATION>(classification)))
                {
                    validation.DoesNotExistOnServer(nameof(classification.Signum));
                }
            }
            return validation;
        }
    }
}