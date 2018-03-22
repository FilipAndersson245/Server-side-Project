using AutoMapper;
using Repository;
using Repository.Support;
using Service.Models;
using Service.Validations;
using System.Collections.Generic;
using System;

namespace Service.Managers
{
    public class ClassificationManager
    {
        private ClassificationRepository _Repo { get; } = new ClassificationRepository();

        public List<Book> GetBooksByClassification(int signId)
        {
            return Mapper.Map<List<BOOK>, List<Book>>(_Repo.GetBooksFromClassification(signId));
        }

        public Classification GetClassificationFromBookIsbn(string isbn)
        {
            return Mapper.Map<Classification>(_Repo.GetClassificationForBook(isbn));
        }

        public Classification GetClassificationFromID(int id)
        {
            return Mapper.Map<CLASSIFICATION, Classification>(_Repo.GetClassificationFromID(id));
        }

        public Classification GetClassificationFromName(string name)
        {
            return Mapper.Map<CLASSIFICATION, Classification>(_Repo.GetClassificationFromName(name));
        }

        public Classification AddGenericClassification()
        {
            if (_Repo.DoesClassificationExist("Generic"))
                return Mapper.Map<CLASSIFICATION, Classification>(_Repo.GetClassificationFromName("Generic"));
            else
            {
                Classification genericClass = new Classification()
                {
                    Signum = "Generic",
                    Description = "Books without a category"
                };
                _Repo.CreateClassification(Mapper.Map<Classification, CLASSIFICATION>(genericClass));
                return genericClass;
            }
        }

        public ClassificationValidation DeleteClassification(Classification classification)
        {
            ClassificationValidation validation = new ClassificationValidation(classification);
            if (_Repo.DoesClassificationContainBooks(Mapper.Map<CLASSIFICATION>(classification)))
                validation.BooksExistInClassification(nameof(classification.Signum));
            else if (!_Repo.DeleteClassification(Mapper.Map<Classification, CLASSIFICATION>(classification)))
                validation.DoesNotExistOnServer(nameof(classification.Signum));
            return validation;
        }

        public ClassificationValidation CreateClassification(Classification classification)
        {
            ClassificationValidation validation = new ClassificationValidation(classification);
            if (validation.IsValid)
                if (!_Repo.CreateClassification(Mapper.Map<Classification, CLASSIFICATION>(classification)))
                    validation.FailedToCreateClassification(nameof(classification.Signum));
            return validation;
        }

        public List<Classification> GetAllClassifications()
        {
            return Mapper.Map<List<CLASSIFICATION>, List<Classification>>(_Repo.GetAllClassifications());
        }

        public ClassificationValidation EditClassification(Classification classification)
        {
            ClassificationValidation validation = new ClassificationValidation(classification);
            if (validation.IsValid)
                if (!_Repo.EditClassification(Mapper.Map<Classification, CLASSIFICATION>(classification)))
                    validation.DoesNotExistOnServer(nameof(classification.Signum));
            return validation;
        }
    }
}