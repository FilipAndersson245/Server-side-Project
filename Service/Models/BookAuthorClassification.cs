using System.Collections.Generic;

namespace Service.Models
{
    public class BookAuthorClassification
    {
        public Book Book { get; set; }

        public List<Author> Authors { get; set; }

        public List<Classification> Classifications { get; set; }
    }
}