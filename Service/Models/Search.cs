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
        IPagedList<Author> AuthorSearchResult { get; set; }

        IPagedList<Book> BookSearchResult { get; set; }

        List<Classification> SelectedClassifications { get; set; }

        string SearchQuery { get; set; }
    }
}
