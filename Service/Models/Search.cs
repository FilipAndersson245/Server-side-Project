using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

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
