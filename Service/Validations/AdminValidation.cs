using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Validations
{
    public class AdminValidation: Validation
    {

        const int MAX_NAME_LENGTH = 30;
        const string PASSWORD_REQ_REGEX = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,25}$";

        public AdminValidation(Admin model)
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
    }
}
