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
                        case 200:
                            modelState.AddModelError(dict.Key, "Some error...");
                            break;
                        case 201:
                            modelState.AddModelError(dict.Key, "Some other error...");
                            break;
                        case 202:
                        case 203:
                            modelState.AddModelError(dict.Key, "many Error with same message");
                            break;
                        case 204:
                            modelState.AddModelError(dict.Key, dict.Key + "is required");
                            break;

                        default:
                            break;
                    }
                }
            }
        }
    }
}
