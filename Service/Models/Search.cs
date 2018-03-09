using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class Search
    {
        List<Author> AuthorSearchResult;

        List<Book> BookSearchResult;

        List<Classification> SelectedClassifications;

        string SearchQuery;
    }
}
