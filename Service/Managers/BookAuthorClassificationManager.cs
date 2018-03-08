using Service.Models;

namespace Service.Managers
{
    public class BookAuthorClassificationManager
    {
        public BookAuthorClassification Setup()
        {
            ClassificationManager classificationManager = new ClassificationManager();
            AuthorManager authorManager = new AuthorManager();
            BookAuthorClassification bac = new BookAuthorClassification();
            bac.Authors = authorManager.GetAllAuthorsToList();
            bac.Classifications = classificationManager.GetAllClassifications();
            return bac;
        }
    }
}