using PagedList;
using System.Collections.Generic;

namespace Service.Models
{
    /// <summary>
    /// Model that allows typing all search result views after the same class, independent of whether you searched for author or book.
    /// </summary>
    public class Search
    {
        public IPagedList<Author> AuthorSearchResult { get; set; }

        public IPagedList<Book> BookSearchResult { get; set; }

        public List<int> SelectedClassifications { get; set; }

        public string SearchQuery { get; set; }
    }
}