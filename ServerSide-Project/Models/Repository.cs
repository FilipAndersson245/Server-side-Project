using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServerSide_Project.Models
{
    public class Repository
    {
        //Tempoary Mockup data. This will be exchanged with real database access later on.
        private List<Book> bookList;

        public Repository()
        {
            bookList = new List<Book>
            {
                new Book {ISBN = "1234", Title = "Fellowship of the Ring", PublicationYear = 1954,
                    Description = "The first book in the Lord of the Rings series.", Pages = 576,
                    Author = new Author { ID = "1", FirstName = "J.R.R", LastName = "Tolkien", BirthYear = 1892},
                    Genre = new Genre { Name = "Fantasy", Art = null } },
                new Book {ISBN = "5678", Title = "The Two Towers", PublicationYear = 1955,
                    Description = "The second book in the Lord of the Rings series.", Pages = 464,
                    Author = new Author { ID = "1", FirstName = "J.R.R", LastName = "Tolkien", BirthYear = 1892},
                    Genre = new Genre { Name = "Fantasy", Art = null } },
                new Book {ISBN = "9876", Title = "Return of the King", PublicationYear = 1956,
                    Description = "The first book in the Lord of the Rings series.", Pages = 432,
                    Author = new Author { ID = "1", FirstName = "J.R.R", LastName = "Tolkien", BirthYear = 1892},
                    Genre = new Genre { Name = "Fantasy", Art = null } },
            };

        }

        public List<Book> BookList { get { return bookList; } set { bookList = value; } }

    }
}