using Service.Models;
using System;

namespace Service.Validations
{
    public class AuthorValidation : Validation
    {
        private const int MAX_NAME_LENGTH = 50;
        private const int MIN_BIRTH_YEAR = -3500;

        public AuthorValidation(Author model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                if (model.FirstName.Length > MAX_NAME_LENGTH)
                {
                    ErrorDict.Add(nameof(model.FirstName), ErrorCodes.TooLong);
                }
            }

            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                ErrorDict.Add(nameof(model.LastName), ErrorCodes.IsRequired);
            }
            else if (model.LastName.Length > MAX_NAME_LENGTH)
            {
                ErrorDict.Add(nameof(model.LastName), ErrorCodes.TooLong);
            }

            if (model.BirthYear != null)
            {
                if (model.BirthYear < MIN_BIRTH_YEAR && model.BirthYear > DateTime.Now.Year - 5)
                {
                    ErrorDict.Add(nameof(model.BirthYear), ErrorCodes.InvalidRange);
                }
            }

            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }
        }
    }
}