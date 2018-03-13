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
        public Book GetBookFromIsbn(string isbn)
        {
            BookRepository repo = new BookRepository();
            var BOOK = repo.GetBookFromIsbn(isbn);
            var book = Mapper.Map<BOOK, Book>(BOOK);
            if (book.Authors.Count == 0)
                book.Authors = AddAuthors(book);
            if (book.Classification == null)
                book.Classification = AddClassification(book);
            return book;
        }

        public List<Author> AddAuthors(Book book)
        {
            List<Author> authors = new List<Author>();
            BookRepository repo = new BookRepository();
            authors = Mapper.Map<List<AUTHOR>, List<Author>>(repo.GetAuthorsFromIsbn(book.ISBN));
            if (authors.Count > 0)
            {
                return authors;
            }
            else
            {
                authors.Add(new Author() { FirstName = "No Author", LastName = "Available", BirthYear = 0, Aid = "-1" });
                return authors;
            }
        }

        public Classification AddClassification(Book book)
        {
            BookRepository bookRepo = new BookRepository();
            ClassificationManager classificationManager = new ClassificationManager();
            Classification classification = Mapper.Map<CLASSIFICATION, Classification>(bookRepo.GetClassificationFromIsbn(Mapper.Map<Book, BOOK>(book)));
            if (classification == null)
            {
                return classificationManager.AddGenericClassification();
            }
            else
            {
                return classification;
            }
        }

        public void SetupBooks(IPagedList<Book> bookList)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                if (bookList[i].SignId == 0 || bookList[i].Classification == null)
                    bookList[i].Classification = AddClassification(bookList[i]);
                if (bookList[i].Authors.Count == 0)
                    bookList[i].Authors = AddAuthors(bookList[i]);
            }
        }

        public Search GetAllBooks(int page, int itemsPerPage)
        {
            BookRepository repo = new BookRepository();
            var bookList = repo.GetAllBooksFromDB(page, itemsPerPage).ToMappedPagedList<BOOK, Book>();
            SetupBooks(bookList);
            Search searchResult = new Search() { BookSearchResult = bookList };
            return searchResult;
        }

        public Search SearchBooks(string search, int page, int itemsPerPage, params int[] classifications)
        {
            BookRepository repo = new BookRepository();
            var bookList = repo.GetBookSearchResultat(search, page, itemsPerPage, classifications).ToMappedPagedList<BOOK, Book>();
            SetupBooks(bookList);
            Search searchResult = new Search() { BookSearchResult = bookList, SearchQuery = search, SelectedClassifications = classifications.ToList()};
            return searchResult;
        }

        public Search SearchBooks(Search search, int page, int itemsPerPage)
        {
            var bookList = new BookRepository().GetBookSearchResultat(search.SearchQuery, page, itemsPerPage, search.SelectedClassifications.ToArray()).ToMappedPagedList<BOOK, Book>();
            SetupBooks(bookList);
            search.BookSearchResult = bookList;
            return search;
        }

        public Tuple<Book, BookValidation> CreateBook(BookAuthorClassification bac, string[] authorChecklist, int? classificationRadio)
        {
            AuthorManager authorManager = new AuthorManager();
            ClassificationManager classificationManager = new ClassificationManager();
            Book book = bac.Book;
            if (classificationRadio == null)
                book.Classification = classificationManager.AddGenericClassification();
            else
                book.Classification = classificationManager.GetClassificationFromID(Convert.ToInt32(classificationRadio));
            book.Authors = new List<Author>();
            if (authorChecklist != null)
            {
                foreach (var aID in authorChecklist)
                {
                    book.Authors.Add(authorManager.GetAuthorFromID(Convert.ToInt32(aID)));
                }
            }
            BookValidation validation = new BookValidation(book);
            if (validation.IsValid)
            {
                BookRepository repo = new BookRepository();
                var newBook = Mapper.Map<BOOK, Book>(repo.CreateBook(Mapper.Map<Book, BOOK>(book)));
                if (newBook != null)
                    return new Tuple<Book, BookValidation>(newBook, validation);
                validation.FailedToCreateBook(nameof(book.Title));
            }
            return new Tuple<Book, BookValidation>(null, validation);
        }

        public bool DeleteBook(string isbn)
        {
            BookRepository repo = new BookRepository();
            return repo.DeleteBook(Mapper.Map<Book, BOOK>(GetBookFromIsbn(isbn)));
        }

        public Tuple<Book, BookValidation> EditBook(BookAuthorClassification bac, string[] authorChecklist, int? classificationRadio)
        {
            AuthorManager authorManager = new AuthorManager();
            ClassificationManager classificationManager = new ClassificationManager();
            Book book = bac.Book;
            BookRepository repo = new BookRepository();
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
            if (!repo.DoesBookExist(book.ISBN))
                validation.BookDoesntExist(book.ISBN);
            else if (validation.IsValid)
            {
                var editedBook = Mapper.Map<BOOK, Book>(repo.EditBook(Mapper.Map<Book, BOOK>(book)));
                if (editedBook != null)
                    return new Tuple<Book, BookValidation>(editedBook, validation);
                validation.FailedToCreateBook(nameof(Book.Title));
            }
            return new Tuple<Book, BookValidation>(null, validation);
        }
    }
}