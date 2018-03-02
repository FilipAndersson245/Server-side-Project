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
        MoreThenSixtyFourChars
    }

    class ValidationModel
    {
        const int BOOK_MAX_SIZE = 3000;
        const string PASSWORD_REQ_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,25}$";
        const int DESCRIPTION_MAX_LENGTH = 1200;
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
            if (!Regex.IsMatch(model.Password, PASSWORD_REQ_REGEX))
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
                if (model.BirthYear < MIN_BIRTH_YEAR && model.BirthYear > DateTime.Now.Year)
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

            //more to come...

            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }
        }

        public ValidationModel(Classification model)
        {
            List<int> listOfErr = new List<int>();

            //more to come...

            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }
        }


        public void DoesNotExistOnServer(string type)
        {
            this.IsValid = false;
            this.ErrorDict.Add(type, ErrorCodes.DoesNotExist); //temp code
        }

        public void DoesAlreadyExistOnServer(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.ExistsAlready); //tmp code
        }

        public void WrongPassword(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.WrongPassword); //tmp code
        }

    }
}
