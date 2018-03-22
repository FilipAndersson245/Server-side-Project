using System.Collections.Generic;

namespace Service.Models
{
    /// <summary>
    /// A model to allow access to all authors and classifications in lists when editing or creating books.
    /// </summary>
    public class BookAuthorClassification
    {
        public Book Book { get; set; }

        public List<Author> Authors { get; set; }

        public List<Classification> Classifications { get; set; }
    }
}