using Service.Models;
using System;

namespace Service.Validations
{
    public class BookValidation : Validation
    {
        private const int BOOK_MAX_LENGTH = 10000;
        private const int TITLE_MAX_LENGTH = 150;
        private const int DESCRIPTION_MAX_LENGTH = 2000;
        private const int MAX_ISBN = 10;
        private const int MIN_ISBN = 4;

        public BookValidation(Book model)
        {
            if (String.IsNullOrWhiteSpace(model.ISBN))
            {
                ErrorDict.Add(nameof(model.ISBN), ErrorCodes.IsRequired);
            }
            else if (model.ISBN.Length > MAX_ISBN || model.ISBN.Length < MIN_ISBN)
            {
                ErrorDict.Add(nameof(model.ISBN), ErrorCodes.InvalidRange);
            }

            if (String.IsNullOrWhiteSpace(model.Title))
            {
                ErrorDict.Add(nameof(model.Title), ErrorCodes.IsRequired);
            }
            else if (model.Title.Length > TITLE_MAX_LENGTH)
            {
                ErrorDict.Add(nameof(model.Title), ErrorCodes.InvalidRange);
            }

            if (model.PublicationYear.ToString().Length > 4)
            {
                ErrorDict.Add(nameof(model.PublicationYear), ErrorCodes.InvalidRange);
            }

            if (model.Publicationinfo != null)
            {
                if (model.Publicationinfo.Length > DESCRIPTION_MAX_LENGTH)
                {
                    ErrorDict.Add(nameof(model.Publicationinfo), ErrorCodes.InvalidRange);
                }
            }

            if (model.Pages > BOOK_MAX_LENGTH)
            {
                ErrorDict.Add(nameof(model.Pages), ErrorCodes.InvalidRange);
            }

            if (model.Classification == null)
            {
                ErrorDict.Add(nameof(model.Classification), ErrorCodes.IsRequired);
            }

            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }
        }
    }
}