using AutoMapper;
using Repository;
using Repository.Support;
using Service.Models;
using Service.Tools;
using Service.Validations;
using System;
using System.Collections.Generic;

namespace Service.Managers
{
    public class AuthorManager
    {
        /// <summary>
        /// Create a new Author
        /// </summary>
        /// <param name="author"></param>
        /// <returns>A tuple with errors and a int? that store if sucessful the created id</returns>
        public Tuple<Author, AuthorValidation> CreateAuthor(Author author)
        {
            AuthorValidation validation = new AuthorValidation(author);
            AuthorRepository repo = new AuthorRepository();
            if (validation.IsValid)
            {
                AUTHOR repoAUTHOR = repo.CreateAuthor(Mapper.Map<Author, AUTHOR>(author));
                if (repoAUTHOR == null)
                {
                    return new Tuple<Author, AuthorValidation>(null, validation);
                }
                else
                {
                    Author newAuthor = Mapper.Map<Author>(repoAUTHOR);
                    if (newAuthor != null)
                    {
                        return new Tuple<Author, AuthorValidation>(newAuthor, validation);
                    }
                    validation.FailedToCreateAuthor(nameof(author.FirstName));
                }
            }
            return new Tuple<Author, AuthorValidation>(null, validation);
        }

        /// <summary>
        /// Edit author return tuple with the edited object if sucessful and a errror dict
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public Tuple<Author, AuthorValidation> EditAuthor(Author author)
        {
            AuthorValidation validation = new AuthorValidation(author);
            if (validation.IsValid)
            {
                AuthorRepository repo = new AuthorRepository();
                var dbAuthor = repo.EditAuthor(Mapper.Map<Author, AUTHOR>(author));
                if (dbAuthor == null)
                {
                    validation.DoesAlreadyExistOnServer(nameof(author.FirstName));
                }
                return new Tuple<Author, AuthorValidation>(Mapper.Map<AUTHOR, Author>(dbAuthor), validation);
            }
            return new Tuple<Author, AuthorValidation>(null, validation);
        }

        public bool DeleteAuthor(Author author)
        {
            AuthorRepository repo = new AuthorRepository();
            return repo.DeleteAuthor(Mapper.Map<Author, AUTHOR>(author));
        }

        public Search GetAllAuthors(int page, int itemsPerPage)
        {
            AuthorRepository repo = new AuthorRepository();
            Search searchResult = new Search() { AuthorSearchResult = repo.GetAllAuthorsFromDB(page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>() };
            return searchResult;
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

        public Search GetAuthorsFromSearch(string search, int page, int itemsPerPage)
        {
            AuthorRepository repo = new AuthorRepository();
            Search searchResult = new Search() { AuthorSearchResult = repo.GetAuthorsFromSearchResult(search, page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>(), SearchQuery = search };
            return searchResult;
        }

        public Search GetAuthorsFromSearch(Search search, int page, int itemsPerPage)
        {
            AuthorRepository repo = new AuthorRepository();
            search.AuthorSearchResult = repo.GetAuthorsFromSearchResult(search.SearchQuery, page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>();
            return search;
        }

        public Author GetAuthorFromID(int id)
        {
            AuthorRepository repo = new AuthorRepository();
            return Mapper.Map<AUTHOR, Author>(repo.GetAuthorFromDB(id));
        }
    }
}