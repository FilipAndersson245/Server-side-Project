using PagedList;
using System.Collections.Generic;

namespace Service.Models
{
    public class Search
    {
        public IPagedList<Author> AuthorSearchResult { get; set; }

        public IPagedList<Book> BookSearchResult { get; set; }

        public List<int> SelectedClassifications { get; set; } //changed from classifications to int

        public string SearchQuery { get; set; }
    }
}