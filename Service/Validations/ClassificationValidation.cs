﻿using Service.Models;
using System;

namespace Service.Validations
{
    public class ClassificationValidation : Validation
    {
        private const int DESCRIPTION_MAX_LENGTH = 255;
        private const int NAME_MAX_LENGTH = 50;

        public ClassificationValidation(Classification model)
        {
            if (String.IsNullOrWhiteSpace(model.Signum))
            {
                ErrorDict.Add(nameof(model.Signum), ErrorCodes.IsRequired);
            }
            else if (model.Signum.Length > NAME_MAX_LENGTH)
            {
                ErrorDict.Add(nameof(model.Signum), ErrorCodes.MoreThanFiftyChars);
            }

            if (string.IsNullOrWhiteSpace(model.Description))
            {
                ErrorDict.Add(nameof(model.Description), ErrorCodes.IsRequired);
            }
            else if (model.Description.Length > DESCRIPTION_MAX_LENGTH)
            {
                ErrorDict.Add(nameof(model.Description), ErrorCodes.MoreThanFiveHundredChars);
            }

            if (ErrorDict.Count == 0)
            {
                IsValid = true;
            }

        }

        public void BooksExistInClassification(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.BooksExistInClassification);
        }
    }
}