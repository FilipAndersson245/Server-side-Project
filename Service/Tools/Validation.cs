using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Tools
{
    class ValidationModel
    {
        const int BOOK_MAX_SIZE = 3000;
        const string PASSWORD_REQ_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,25}$";
        const int DESCRIPTION_MAX_LENGTH = 1200;
        const int MAX_NAME_LENGTH = 25;

        public bool IsValid { get; set; } = false;

        public Dictionary<string, int> ErrorDict { get; set; } = new Dictionary<string, int>();

        public ValidationModel(Admin model)
        {
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                ErrorDict.Add(nameof(model.Username), 1);
            }
            else if (model.Username.Length > MAX_NAME_LENGTH)
            {
                ErrorDict.Add(nameof(model.Username), 2);
            }
            if (!Regex.IsMatch(model.Password, PASSWORD_REQ_REGEX))
            {
                ErrorDict.Add(nameof(model.Password), 3);
            }

            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }
        }

        public ValidationModel(Author model)
        {

            if (string.IsNullOrWhiteSpace(model.Aid))
            {
                ErrorDict.Add(nameof(model.Aid), 1);
            }
            else if (model.Aid.Length > 5)
            {
                ErrorDict.Add(nameof(model.Aid), 5);
            }

            if(string.IsNullOrWhiteSpace(model.FirstName))
            {
                ErrorDict.Add(nameof(model.FirstName), 1);
            }
            else if (model.FirstName.Length > MAX_NAME_LENGTH)
            {
                ErrorDict.Add(nameof(model.FirstName), 2);
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                ErrorDict.Add(nameof(model.LastName), 1);
            }
            else if (model.FirstName.Length > MAX_NAME_LENGTH)
            {
                ErrorDict.Add(nameof(model.LastName), 2);
            }
            //... more to come

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
            this.ErrorDict.Add(type, 123); //temp code
        }

        public void DoesAlreadyExistOnServer(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, 124); //tmp code
        }

        public void WrongPassword(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, 125); //tmp code
        }

    }
}
