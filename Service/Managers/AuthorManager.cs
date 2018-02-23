using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using Repository.Support;
using Repository;
using AutoMapper;
using PagedList;
using Service.Models;
using Service.Tools;

namespace Service.Managers
{
    public class AuthorManager
    {
        public static int CreateAuthor(Author author) //Returns Aid if successfull, 0 if failed
        {
            return AuthorRepository.CreateAuthor(Mapper.Map<Author, AUTHOR>(author));
        }

        public static Author updateAuthor(Author author)
        {
            return Mapper.Map<AUTHOR, Author>(AuthorRepository.updateAuthor(Mapper.Map<Author, AUTHOR>(author)));
        }

        public static bool deleteAuthor(Author author)
        {
            return AuthorRepository.deleteAuthor(Mapper.Map<Author, AUTHOR>(author));
        }

        public static IPagedList<Author> getAllAuthors(int page, int itemsPerPage)
        {
            return AuthorRepository.getAllAuthorsFromDB(page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>();
        }

        public static Author getAuthorDetails(int id, int bookPage)
        {
            Author author = Mapper.Map<AUTHOR, Author>(AuthorRepository.getAuthorDetailsFromDB(id));
            author.BookList = AuthorRepository.getBooksByAuthor(id, bookPage).ToMappedPagedList<BOOK, Book>();
            BookManager.setupBooks(author.BookList);
            return author;
        }

        public static IPagedList<Author> getAuthorsFromSearch(string search, int page, int itemsPerPage)
        {
            return AuthorRepository.getAuthorsFromSearchResult(search, page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>(); ;
        }

        public static Author getAuthorFromID(int id)
        {
            Author author = Mapper.Map<AUTHOR, Author>(AuthorRepository.getAuthorFromDB(id));
            return author;
        }



    }
}
