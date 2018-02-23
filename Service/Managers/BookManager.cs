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
        public static Book getBookFromIsbn(string isbn)
        {
            var book = Mapper.Map<BOOK, Book>(BookRepository.getBookFromIsbn(isbn));
            book.Authors = addAuthors(book);
            return book;
        }

        public static List<Author> addAuthors(Book book)
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

        public static void setupBooks(IPagedList<Book> bookList)
        {
            for (int i = 0; i < bookList.Count; i++)
            {
                bookList[i].Authors = addAuthors(bookList[i]);
            }
        }

        public static IPagedList<Book> getAllBooks(int page, int itemsPerPage)
        {
            var bookList = BookRepository.getAllBooksFromDB(page, itemsPerPage).ToMappedPagedList<BOOK, Book>();
            setupBooks(bookList);
            return bookList;
        }

        public static IPagedList<Book> SearchBooks(string search, int page, int itemsPerPage, params int[] classifications)
        {
            var bookList = BookRepository.GetBookSearchResultat(search, page, itemsPerPage, classifications).ToMappedPagedList<BOOK, Book>();
            setupBooks(bookList);
            return bookList;
        }

        public static Book createBook(Book book)
        {
            return Mapper.Map<BOOK, Book>(BookRepository.createBook(Mapper.Map<Book, BOOK>(book)));
        }

        public static bool deleteBook(string isbn)
        {
            return BookRepository.deleteBook(Mapper.Map<Book, BOOK>(getBookFromIsbn(isbn)));
        }

        public static Book editBook(Book book)
        {
            return Mapper.Map<BOOK, Book>(BookRepository.editBook(Mapper.Map<Book, BOOK>(book)));
        }




    }
}
