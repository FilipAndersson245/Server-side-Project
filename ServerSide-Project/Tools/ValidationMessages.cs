using Service.Validations;
using System.Collections.Generic;

namespace ServerSide_Project.Tools
{
    public class ValidationMessages
    {
        //public ModelStateDictionary modelState = new ModelStateDictionary();

        public static void ConvertCodeToMsg(System.Web.Mvc.ModelStateDictionary modelState, Dictionary<string, ErrorCodes> MessageCodes)
        {
            foreach (var dict in MessageCodes)
            {
                var code = dict.Value;
                switch (code)
                {
                    case ErrorCodes.IsRequired:
                        modelState.AddModelError(dict.Key, " Is Required!");
                        break;

                    case ErrorCodes.DoesNotExist:
                        modelState.AddModelError(dict.Key, " Does not exist");
                        break;

                    case ErrorCodes.ExistsAlready:
                        modelState.AddModelError(dict.Key, " Is already in use");
                        break;

                    case ErrorCodes.InvalidRange:
                        modelState.AddModelError(dict.Key, "Invalid range");
                        break;

                    case ErrorCodes.NotANumber:
                        modelState.AddModelError(dict.Key, "Not a number");
                        break;

                    case ErrorCodes.NotValidPageNr:
                        modelState.AddModelError(dict.Key, "Not a valid page number");
                        break;

                    case ErrorCodes.ToLong:
                        modelState.AddModelError(dict.Key, "To long");
                        break;

                    case ErrorCodes.MustBeTenCharLong:
                        modelState.AddModelError(dict.Key, "Must be 10 char long");
                        break;

                    case ErrorCodes.MoreThenFiveHundredChars:
                        modelState.AddModelError(dict.Key, "To long cannot be longer then 500 chars");
                        break;

                    case ErrorCodes.MoreThenSixtyFourChars:
                        modelState.AddModelError(dict.Key, "To long cannot be longer then 64 chars");
                        break;

                    case ErrorCodes.InsuficentPermission:
                        modelState.AddModelError(dict.Key, "Insuficent permission");
                        break;

                    case ErrorCodes.PasswordDoesNotMatch:
                        modelState.AddModelError(dict.Key, "Password must be between 5-25 chars. Have atleast 1 upper and lower char and contain atleast one number");
                        break;

                    case ErrorCodes.WrongPassword:
                        modelState.AddModelError(dict.Key, "Wrong Password");
                        break;

                    default:
                        modelState.AddModelError(dict.Key, "Unspecified error code:" + code.ToString() + " Pls contact the dev team");
                        break;
                }
            }
        }
    }
}