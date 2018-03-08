using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Models;

namespace Service.Validations
{
    public class ClassificationValidation: Validation
    {

        const int DESCRIPTION_MAX_LENGTH = 500;

        public ClassificationValidation(Classification model)
        {
            if (String.IsNullOrWhiteSpace(model.Signum))
            {
                ErrorDict.Add(nameof(model.Signum), ErrorCodes.IsRequired);
            }
            else if (model.Signum.Length < 64)
            {
                ErrorDict.Add(nameof(model.Signum), ErrorCodes.MoreThenSixtyFourChars);
            }

            if (string.IsNullOrWhiteSpace(model.Description))
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

    }
}
