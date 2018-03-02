using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Helpers;
using Repository.Support;
using Repository;
using AutoMapper;
using PagedList;
using Service.Models;
using Service.Tools;

namespace Service.Managers
{
    public class BookManager
    {
        public Book GetBookFromIsbn(string isbn)
        {
            BookRepository repo = new BookRepository();
            var book = Mapper.Map<BOOK, Book>(repo.GetBookFromIsbn(isbn));
            book.Authors = AddAuthors(book);
            book.BookClassification = AddClassification(book);
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
            ClassificationRepository classRepo = new ClassificationRepository();
            Classification classification = Mapper.Map<CLASSIFICATION, Classification>(bookRepo.GetClassificationFromIsbn(Mapper.Map<Book, BOOK>(book)));
            if (classification == null)
            {
                if (classRepo.DoesClassificationExist("Generic"))
                {
                    return Mapper.Map<CLASSIFICATION, Classification>(classRepo.GetClassificationFromName("Generic"));
                }
                else
                {
                    Classification genericClass = new Classification() { Signum = "Generic", Description = "Books without a category" };
                    if (classRepo.CreateClassification(Mapper.Map<Classification, CLASSIFICATION>(genericClass)))
                    {
                        return genericClass;
                    }
                    else
                    {
                        return genericClass; //Add some warning for user maybe
                    }
                }
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
                bookList[i].Authors = AddAuthors(bookList[i]);
                bookList[i].BookClassification = AddClassification(bookList[i]);
            }
        }

        public IPagedList<Book> GetAllBooks(int page, int itemsPerPage)
        {
            BookRepository repo = new BookRepository();
            var bookList = repo.GetAllBooksFromDB(page, itemsPerPage).ToMappedPagedList<BOOK, Book>();
            //SetupBooks(bookList);
            return bookList;
        }

        public IPagedList<Book> SearchBooks(string search, int page, int itemsPerPage, params int[] classifications)
        {
            BookRepository repo = new BookRepository();
            var bookList = repo.GetBookSearchResultat(search, page, itemsPerPage, classifications).ToMappedPagedList<BOOK, Book>();
            SetupBooks(bookList);
            return bookList;
        }

        public Book CreateBook(Book book)
        {
            BookRepository repo = new BookRepository();
            var newBook = Mapper.Map<BOOK, Book>(repo.CreateBook(Mapper.Map<Book, BOOK>(book)));
            newBook.Authors = AddAuthors(newBook);
            newBook.BookClassification = AddClassification(newBook);
            return newBook;
        }

        public bool DeleteBook(string isbn)
        {
            BookRepository repo = new BookRepository();
            return repo.DeleteBook(Mapper.Map<Book, BOOK>(GetBookFromIsbn(isbn)));
        }

        public Book EditBook(Book book)
        {
            BookRepository repo = new BookRepository();
            return Mapper.Map<BOOK, Book>(repo.EditBook(Mapper.Map<Book, BOOK>(book)));
        }

    }
}
