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

        public static Author EditAuthor(Author author)
        {
            return Mapper.Map<AUTHOR, Author>(AuthorRepository.EditAuthor(Mapper.Map<Author, AUTHOR>(author)));
        }

        public static bool DeleteAuthor(Author author)
        {
            return AuthorRepository.DeleteAuthor(Mapper.Map<Author, AUTHOR>(author));
        }

        public static IPagedList<Author> GetAllAuthors(int page, int itemsPerPage)
        {
            return AuthorRepository.GetAllAuthorsFromDB(page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>();
        }

        public static Author GetAuthorDetails(int id, int bookPage)
        {
            Author author = Mapper.Map<AUTHOR, Author>(AuthorRepository.GetAuthorDetailsFromDB(id));
            BookManager.SetupBooks(AuthorRepository.GetBooksByAuthor(id, bookPage).ToMappedPagedList<BOOK, Book>());
            return author;
        }

        public static IPagedList<Author> GetAuthorsFromSearch(string search, int page, int itemsPerPage)
        {
            return AuthorRepository.GetAuthorsFromSearchResult(search, page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>(); ;
        }

        public static Author GetAuthorFromID(int id)
        {
            return Mapper.Map<AUTHOR, Author>(AuthorRepository.GetAuthorFromDB(id));
        }

    }
}
