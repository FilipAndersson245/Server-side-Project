using Service.Models;
using System;

namespace Service.Validations
{
    public class AuthorValidation : Validation
    {
        private const int MAX_NAME_LENGTH = 30;
        private const int MIN_BIRTH_YEAR = -3500;

        public AuthorValidation(Author model)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                ErrorDict.Add(nameof(model.FirstName), ErrorCodes.IsRequired);
            }
            else if (model.FirstName.Length > MAX_NAME_LENGTH)
            {
                ErrorDict.Add(nameof(model.FirstName), ErrorCodes.ToLong);
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                ErrorDict.Add(nameof(model.LastName), ErrorCodes.IsRequired);
            }
            else if (model.FirstName.Length > MAX_NAME_LENGTH)
            {
                ErrorDict.Add(nameof(model.LastName), ErrorCodes.ToLong);
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