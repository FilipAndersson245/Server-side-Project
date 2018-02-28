using System.Collections.Generic;
using System.Web.ModelBinding;

namespace ServerSide_Project.Tools
{
    public class ValidationMessages
    {
        public ModelStateDictionary modelState = new ModelStateDictionary();

        public ValidationMessages(Dictionary<string, List<int>> MessageCodes)
        {
            foreach(var dict in MessageCodes)
            {
                foreach (var code in dict.Value)
                {
                    switch (code)
                    {
                        case 100:
                            modelState.AddModelError(dict.Key, " Is Required!");
                            break;
                        case 110:
                            modelState.AddModelError(dict.Key, " Does not exist");
                            break;
                        case 120:
                            modelState.AddModelError(dict.Key, " Is already in use");
                            break;
                        case 130:
                            modelState.AddModelError(dict.Key, "Invalid range");
                            break;
                        case 131:
                            modelState.AddModelError(dict.Key, "Must be between -3500 and "+ System.DateTime.Now.Year.ToString());
                            break;
                        case 132:
                            modelState.AddModelError(dict.Key, "Not a number");
                            break;
                        case 133:
                            modelState.AddModelError(dict.Key, "Not a valid page number");
                            break;
                        case 140:
                            modelState.AddModelError(dict.Key, "To long");
                            break;
                        case 141:
                            modelState.AddModelError(dict.Key, "Must be 10 char long");
                            break;
                        case 142:
                            modelState.AddModelError(dict.Key, "To long cannot be longer then 500 chars");
                            break;
                        case 143:
                            modelState.AddModelError(dict.Key, "To long cannot be longer then 64 chars");
                            break;
                        case 150:
                            modelState.AddModelError(dict.Key, "Insuficent permission");
                            break;
                        case 160:
                            modelState.AddModelError(dict.Key, "Password must be between 5-25 chars. Have atleast 1 upper and lower char and contain atleast one number");
                            break;
                        case 161:
                            modelState.AddModelError(dict.Key, "");
                            break;
                        default:
                            throw new System.Exception("Error code does not exist with validation message");
                    }
                }
            }
        }
    }
}
