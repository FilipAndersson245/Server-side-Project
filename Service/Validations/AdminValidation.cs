﻿using Service.Models;
using System.Text.RegularExpressions;

namespace Service.Validations
{
    public class AdminValidation : Validation
    {
        private const int MAX_NAME_LENGTH = 64;
        private const string PASSWORD_REQ_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,25}$";

        public AdminValidation(Admin model)
        {
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                ErrorDict.Add(nameof(model.Username), ErrorCodes.IsRequired);
            }
            else if (model.Username.Length > MAX_NAME_LENGTH)
            {
                ErrorDict.Add(nameof(model.Username), ErrorCodes.TooLong);
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
    }
}