using Service.Models;

namespace Service.Managers
{
    public class BookAuthorClassificationManager
    {
        public BookAuthorClassification Setup()
        {
            ClassificationManager classificationManager = new ClassificationManager();
            AuthorManager authorManager = new AuthorManager();
            BookAuthorClassification bac = new BookAuthorClassification
            {
                Authors = authorManager.GetAllAuthorsToList(),
                Classifications = classificationManager.GetAllClassifications()
            };
            return bac;
        }
    }
}