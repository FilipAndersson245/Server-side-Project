using PagedList;

namespace Service.Models
{
    public class Author
    {
        public Author()
        {
        }

        public string Aid { get; set; } //Primary Key

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? BirthYear { get; set; } = null;

        public IPagedList<Book> BookList { get; set; }

        public string FullName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
            }
        }
    }
}