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
        public static Book GetBookFromIsbn(string isbn)
        {
            var book = Mapper.Map<BOOK, Book>(BookRepository.GetBookFromIsbn(isbn));
            book.Authors = AddAuthors(book);
            return book;
        }

        public static List<Author> AddAuthors(Book book)
        {
            List<Author> authors = new List<Author>();
            authors = Mapper.Map<List<AUTHOR>, List<Author>>(BookRepository.GetAuthorsFromIsbn(book.ISBN));
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

        public static void SetupBooks(IPagedList<Book> bookList)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                bookList[i].Authors = AddAuthors(bookList[i]);
            }
        }

        public static IPagedList<Book> GetAllBooks(int page, int itemsPerPage)
        {
            var bookList = BookRepository.GetAllBooksFromDB(page, itemsPerPage).ToMappedPagedList<BOOK, Book>();
            SetupBooks(bookList);
            return bookList;
        }

        public static IPagedList<Book> SearchBooks(string search, int page, int itemsPerPage, params int[] classifications)
        {
            var bookList = BookRepository.GetBookSearchResultat(search, page, itemsPerPage, classifications).ToMappedPagedList<BOOK, Book>();
            SetupBooks(bookList);
            return bookList;
        }

        public static Book CreateBook(Book book)
        {
            return Mapper.Map<BOOK, Book>(BookRepository.CreateBook(Mapper.Map<Book, BOOK>(book)));
        }

        public static bool DeleteBook(string isbn)
        {
            return BookRepository.DeleteBook(Mapper.Map<Book, BOOK>(GetBookFromIsbn(isbn)));
        }

        public static Book EditBook(Book book)
        {
            return Mapper.Map<BOOK, Book>(BookRepository.EditBook(Mapper.Map<Book, BOOK>(book)));
        }

    }
}
