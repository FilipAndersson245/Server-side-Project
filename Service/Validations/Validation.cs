using System.Collections.Generic;

namespace Service.Validations
{
    public enum ErrorCodes
    {
        IsRequired = 1,
        DoesNotExist,
        InUse,
        InvalidRange,
        NotANumber,
        TooLong,
        InsufficentPermission,
        PasswordDoesNotMatch,
        ExistsAlready,
        WrongPassword,
        NotValidPageNr,
        MoreThanFiveHundredChars,
        MoreThanFiftyChars,
        FailedToCreateAuthor,
        FailedToCreateBook,
        BookDoesntExist,
        FailedToCreateClassification,
        BooksExistInClassification,
        MustBeOnlyNumbers
    }

    public class Validation
    {
        public bool IsValid { get; set; } = false;

        public Dictionary<string, ErrorCodes> ErrorDict { get; set; } = new Dictionary<string, ErrorCodes>();

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

        public void FailedToCreateClassification(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.FailedToCreateClassification);
        }

        public void MustBeOnlyNumbers(string type)
        {
            IsValid = false;
            ErrorDict.Add(type, ErrorCodes.MustBeOnlyNumbers);
        }

    }
}