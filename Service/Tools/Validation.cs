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
        const string PASSWORD_REQ_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{5,15}$";
        const int DESCRIPTION_MAX_LENGTH = 1200;
        const int MAX_NAME_LENGTH = 25;

        public bool IsValid { get; set; } = false;

        public Dictionary<string, List<int>> ErrorList { get; set; }

        public ValidationModel(Admin model)
        {
            List<int> listOfErr = new List<int>();
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                listOfErr.Add(1);
            }
            else if (model.Username.Length > MAX_NAME_LENGTH)
            {
                listOfErr.Add(2);
            }
            if (Regex.IsMatch(model.Password, PASSWORD_REQ_REGEX))
            {
                listOfErr.Add(3);
            }

            if (listOfErr.Count == 0)
            {
                IsValid = true;
            }
        }

        public ValidationModel(Author model)
        {
            List<int> listOfErr = new List<int>();

            if (string.IsNullOrWhiteSpace(model.Aid))
            {
                listOfErr.Add(1);
            }
            else if (model.Aid.Length > 5)
            {
                listOfErr.Add(5);
            }

            if(string.IsNullOrWhiteSpace(model.FirstName))
            {
                listOfErr.Add(1);
            }
            else if (model.FirstName.Length > MAX_NAME_LENGTH)
            {
                listOfErr.Add(2);
            }

            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                listOfErr.Add(1);
            }
            else if (model.FirstName.Length > MAX_NAME_LENGTH)
            {
                listOfErr.Add(2);
            }

            if (listOfErr.Count == 0)
            {
                IsValid = true;
            }
        }

        public ValidationModel(Book model)
        {
            List<int> listOfErr = new List<int>();



            if (listOfErr.Count == 0)
            {
                IsValid = true;
            }
        }

        public ValidationModel(Classification model)
        {
            List<int> listOfErr = new List<int>();



            if (listOfErr.Count == 0)
            {
                IsValid = true;
            }
        }

    }
}
