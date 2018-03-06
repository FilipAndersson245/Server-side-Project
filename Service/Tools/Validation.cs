using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Tools
{
    public enum ErrorCodes
    {
        IsRequired = 1,
        DoesNotExist,
        InUse,
        InvalidRange,
        NotANumber,
        ToLong,
        MustBeTenCharLong,
        InsuficentPermission,
        PasswordDoesNotMatch,
        ExistsAlready,
        WrongPassword,
        NotValidPageNr,
        MoreThenFiveHundredChars,
        MoreThenSixtyFourChars,
        FailedToCreateAuthor,
        FailedToCreateBook,
        BookDoesntExist
    }

    public class ValidationModel
    {
        const int BOOK_MAX_SIZE = 3000;
        const string PASSWORD_REQ_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,25}$";
        const int DESCRIPTION_MAX_LENGTH = 500;
        const int MAX_NAME_LENGTH = 30;
        const int MIN_BIRTH_YEAR = -3500;

        public bool IsValid { get; set; } = false;

        public Dictionary<string, ErrorCodes> ErrorDict { get; set; } = new Dictionary<string, ErrorCodes>();

        public ValidationModel(Admin model)
        {
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                ErrorDict.Add(nameof(model.Username), ErrorCodes.IsRequired);
            }
            else if (model.Username.Length > MAX_NAME_LENGTH)
            {
                ErrorDict.Add(nameof(model.Username), ErrorCodes.ToLong);
            }
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ErrorDict.Add(nameof(model.Password), ErrorCodes.IsRequired);
            }
            else if (!Regex.IsMatch(model.Password, PASSWORD_REQ_REGEX))
            {
                ErrorDict.Add(nameof(model.Password), ErrorCodes.PasswordDoesNotMatch);
            }

            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }
        }

        public ValidationModel(Author model)
        {
            if(string.IsNullOrWhiteSpace(model.FirstName))
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
                if (model.BirthYear < MIN_BIRTH_YEAR && model.BirthYear > DateTime.Now.Year-5)
                {
                    ErrorDict.Add(nameof(model.BirthYear), ErrorCodes.InvalidRange);
                }
            }
            
            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }
        }

        public ValidationModel(Book model)
        {

            if (String.IsNullOrWhiteSpace(model.ISBN))
            {
                ErrorDict.Add(nameof(model.ISBN), ErrorCodes.IsRequired);
            }
            else if(model.ISBN.Length > 10 || model.ISBN.Length < 4)
            {
                ErrorDict.Add(nameof(model.ISBN), ErrorCodes.InvalidRange);
            }

            if (String.IsNullOrWhiteSpace(model.Title))
            {
                ErrorDict.Add(nameof(model.Title), ErrorCodes.IsRequired);
            }
            else if (model.Title.Length > 150)
            {
                ErrorDict.Add(nameof(model.Title), ErrorCodes.InvalidRange);
            }

            if (model.PublicationYear.ToString().Length > 4)
            {
                ErrorDict.Add(nameof(model.PublicationYear), ErrorCodes.InvalidRange);
            }
            
            if (model.Publicationinfo.Length > 2000)
            {
                ErrorDict.Add(nameof(model.Publicationinfo), ErrorCodes.InvalidRange);
            }

            if (model.Pages > 10000)
            {
                ErrorDict.Add(nameof(model.Pages), ErrorCodes.InvalidRange);
            }

            if (model.Authors != null)
            {
                if (model.Authors.Count < 1)
                {
                    ErrorDict.Add(nameof(model.Authors), ErrorCodes.IsRequired);
                }
                else if (model.Authors.Count > 15)
                {
                    ErrorDict.Add(nameof(model.Authors), ErrorCodes.InvalidRange);
                }
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

        public ValidationModel(Classification model)
        {
            if (String.IsNullOrWhiteSpace(model.Signum))
            {
                ErrorDict.Add(nameof(model.Signum), ErrorCodes.IsRequired);
            }
            else if(model.Signum.Length < 64)
            {
                ErrorDict.Add(nameof(model.Signum), ErrorCodes.MoreThenSixtyFourChars);
            }

            if(string.IsNullOrWhiteSpace(model.Description))
            {
                ErrorDict.Add(nameof(model.Description), ErrorCodes.IsRequired);
            }
            else if (model.Description.Length > DESCRIPTION_MAX_LENGTH)
            {
                ErrorDict.Add(nameof(model.Description), ErrorCodes.MoreThenFiveHundredChars);
            }

            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }
        }


        public void DoesNotExistOnServer(string type)
        {
            this.IsValid = false;
            this.ErrorDict.Add(type, ErrorCodes.DoesNotExist);
        }

        public void DoesAlreadyExistOnServer(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.ExistsAlready);
        }

        public void WrongPassword(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.WrongPassword);
        }

        public void FailedToCreateAuthor(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.FailedToCreateAuthor);
        }

        public void FailedToCreateBook(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.FailedToCreateBook);
        }

        public void BookDoesntExist(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.BookDoesntExist);
        }
    }
}
