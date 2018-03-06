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
        public int? CreateAuthor(ModelStateDictionary modelState,Author author) //Returns Aid if successfull, 0 if failed
        {
            //todo remove ModelStateDictionary

            ValidationModel validation = new ValidationModel(author);

            int? id = null;
            if (validation.IsValid)
            {
                AuthorRepository repo = new AuthorRepository();
                id = repo.CreateAuthor(Mapper.Map<Author, AUTHOR>(author));
                if (id == null)
                {
                    validation.FailedToCreateAuthor(nameof(author.FirstName));
                }
            }
            return id;
            //should return the validation instead of int? should maybe Tuple.
        }

        public Author EditAuthor(ModelStateDictionary modelState, Author author)
        {
            //todo remove ModelStateDictionary

            ValidationModel validation = new ValidationModel(author);
            if (validation.IsValid)
            {
                AuthorRepository repo = new AuthorRepository();
                var dbAuthor = repo.EditAuthor(Mapper.Map<Author, AUTHOR>(author));
                if (dbAuthor == null)
                {
                    modelState.AddModelError("", "Author does not exist on the database");
                    validation.DoesAlreadyExistOnServer(nameof(author.FirstName));
                }
                return Mapper.Map<AUTHOR, Author>(dbAuthor);
            }
            return null;
            //return tuple probebly same as above with id?
            
        }

        public bool DeleteAuthor(Author author)
        {
            AuthorRepository repo = new AuthorRepository();
            return repo.DeleteAuthor(Mapper.Map<Author, AUTHOR>(author));
        }

        public IPagedList<Author> GetAllAuthors(int page, int itemsPerPage)
        {
            AuthorRepository repo = new AuthorRepository();
            return repo.GetAllAuthorsFromDB(page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>();
        }

        public List<Author> GetAllAuthorsToList()
        {
            AuthorRepository repo = new AuthorRepository();
            return Mapper.Map<List<AUTHOR>, List<Author>>(repo.GetAllAuthorsFromDBToList());
        }

        public Author GetAuthorDetails(int id, int bookPage)
        {
            AuthorRepository repo = new AuthorRepository();
            Author author = Mapper.Map<AUTHOR, Author>(repo.GetAuthorDetailsFromDB(id));
            author.BookList = repo.GetBooksByAuthor(id, bookPage).ToMappedPagedList<BOOK, Book>();

            BookManager bookManager = new BookManager();
            bookManager.SetupBooks(author.BookList);
            return author;
        }

        public IPagedList<Author> GetAuthorsFromSearch(string search, int page, int itemsPerPage)
        {
            AuthorRepository repo = new AuthorRepository();
            return repo.GetAuthorsFromSearchResult(search, page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>(); ;
        }

        public Author GetAuthorFromID(int id)
        {
            AuthorRepository repo = new AuthorRepository();
            return Mapper.Map<AUTHOR, Author>(repo.GetAuthorFromDB(id));
        }

    }
}
