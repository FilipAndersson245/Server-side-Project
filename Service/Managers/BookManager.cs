using AutoMapper;
using PagedList;
using Repository;
using Repository.Support;
using Service.Models;
using Service.Tools;
using Service.Validations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Managers
{
    public class BookManager
    {
        private BookRepository _Repo { get; } = new BookRepository();

        public Book GetBookFromIsbn(string isbn)
        {
            var dbBOOK = _Repo.GetBookFromIsbn(isbn);
            var book = Mapper.Map<BOOK, Book>(dbBOOK);
            if (book.Authors.Count == 0)
                book.Authors = AddAuthors(book); //add unknown
            if (book.Classification == null)
                book.Classification = AddClassification(book); //add generic
            return book;
        }

        /// <summary>
        /// return list of authors from a given book
        /// if nonexistant add No Author Available
        /// </summary>
        /// <param name="book">a book with a valid isbn</param>
        /// <returns>A list of authors with atleast one item</returns>
        public List<Author> AddAuthors(Book book)
        {
            var authors = Mapper.Map<List<AUTHOR>, List<Author>>(_Repo.GetAuthorsFromIsbn(book.ISBN));
            if (authors.Count == 0)
            {
                authors.Add(new Author() { FirstName = "No Author", LastName = "Available", BirthYear = 0, Aid = "-1" });
            }
            return authors;
        }

        /// <summary>
        /// Add classification to a book.
        /// If non exust add generic classification
        /// </summary>
        public Classification AddClassification(Book book)
        {
            ClassificationManager classificationManager = new ClassificationManager();
            var classification = Mapper.Map<CLASSIFICATION, Classification>(_Repo.GetClassificationFromIsbn(Mapper.Map<Book, BOOK>(book)));
            if (classification == null)
            {
                return classificationManager.AddGenericClassification();
            }
            else
            {
                return classification;
            }
        }

        /// <summary>
        /// Setup a multitude of books with classification and author.
        /// </summary>
        public void SetupBooks(IPagedList<Book> bookList)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].SignId == 0 || bookList[i].Classification == null)
                {
                    bookList[i].Classification = AddClassification(bookList[i]);
                    bookList[i].SignId = bookList[i].Classification.SignId;
                    _Repo.EditBook(Mapper.Map<BOOK>(bookList[i]));
                }
                if (bookList[i].Authors.Count == 0)
                    bookList[i].Authors = AddAuthors(bookList[i]);
            }
        }

        /// <summary>
        /// Get all books from db in a page
        /// </summary>
        /// <returns>A Search object with its booklist set to all books in a page</returns>
        public Search GetAllBooks(int page, int itemsPerPage)
        {
            var bookList = _Repo.GetAllBooksFromDB(page, itemsPerPage).ToMappedPagedList<BOOK, Book>();
            SetupBooks(bookList);
            return new Search() { BookSearchResult = bookList };
        }

        /// <summary>
        /// Require a Search query and returns
        /// all matching resultats
        /// </summary>
        public List<Book> GetSearchedBooksToList(Search search)
        {
            return Mapper.Map<List<Book>>(_Repo.GetSearchedBooksFromDBToList(search.SearchQuery));
        }

        public Search SearchBooks(string search, int page, int itemsPerPage, params int[] classifications)
        {
            if (search == null) //prevent bug where null matches 0 resultat
                search = "";
            var bookList = _Repo.GetBookSearchResultat(search, page, itemsPerPage, classifications).ToMappedPagedList<BOOK, Book>();
            SetupBooks(bookList);
            return new Search() // create Search object with existing data and returns it
            {
                BookSearchResult = bookList,
                SearchQuery = search,
                SelectedClassifications = classifications != null ? classifications.ToList() : null //do convertion to list if not null
            };
        }

        /// <summary>
        /// Return a Tuple with the Book
        /// And a validation object
        /// </summary>
        /// <returns></returns>
        public Tuple<Book, BookValidation> CreateBook(BookAuthorClassification bookAuthorClassification, string[] authorChecklist, int? classificationRadio)
        {
            AuthorManager authorManager = new AuthorManager();
            ClassificationManager classificationManager = new ClassificationManager();


            Book book = bookAuthorClassification.Book;
            book.Authors = new List<Author>();
            if (classificationRadio == null)
                book.Classification = classificationManager.AddGenericClassification();
            else
                book.Classification = classificationManager.GetClassificationFromID(Convert.ToInt32(classificationRadio));
            if (authorChecklist != null)
            {
                foreach (var aId in authorChecklist)
                {
                    book.Authors.Add(authorManager.GetAuthorFromID(Convert.ToInt32(aId)));
                }
            }

            BookValidation validation = new BookValidation(book);
            if (validation.IsValid)
            {
                var repoBOOK = _Repo.CreateBook(Mapper.Map<Book, BOOK>(book));
                if (repoBOOK == null)
                {
                    return new Tuple<Book, BookValidation>(null, validation);
                }
                else
                {
                    Book newBook = Mapper.Map<Book>(repoBOOK);
                    if (newBook != null)
                        return new Tuple<Book, BookValidation>(newBook, validation);
                    validation.FailedToCreateBook(nameof(book.Title));
                }
            }
            return new Tuple<Book, BookValidation>(null, validation);
        }

        public bool DeleteBook(string isbn)
        {
            return _Repo.DeleteBook(Mapper.Map<Book, BOOK>(GetBookFromIsbn(isbn)));
        }
        /// <summary>
        /// Returns a Tuple with Book and a Validation object
        /// </summary>
        public Tuple<Book, BookValidation> EditBook(BookAuthorClassification bac, string[] authorChecklist, int? classificationRadio)
        {
            AuthorManager authorManager = new AuthorManager();
            ClassificationManager classificationManager = new ClassificationManager();
            Book book = bac.Book;
            if (classificationRadio == null)
            {
                book.Classification = classificationManager.AddGenericClassification();
                book.SignId = book.Classification.SignId;
            }
            else
            {
                book.Classification = classificationManager.GetClassificationFromID(Convert.ToInt32(classificationRadio));
                book.SignId = book.Classification.SignId;
            }
            book.Authors = new List<Author>();
            if (authorChecklist != null)
            {
                foreach (var aID in authorChecklist)
                {
                    book.Authors.Add(authorManager.GetAuthorFromID(Convert.ToInt32(aID)));
                }
            }
            BookValidation validation = new BookValidation(book);
            if (!_Repo.DoesBookExist(book.ISBN))
                validation.BookDoesntExist(book.ISBN);
            else if (validation.IsValid)
            {
                BOOK repoBOOK = _Repo.EditBook(Mapper.Map<BOOK>(book));
                if (repoBOOK != null)
                {
                    Book editedBook = Mapper.Map<Book>(repoBOOK);
                    if (editedBook != null)
                        return new Tuple<Book, BookValidation>(editedBook, validation);
                }
                validation.FailedToCreateBook(nameof(Book.Title));
            }
            return new Tuple<Book, BookValidation>(null, validation);
        }
    }
}