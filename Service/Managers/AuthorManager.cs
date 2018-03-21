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
        private AuthorRepository _Repo { get; } = new AuthorRepository();

        /// <summary>
        /// Create a new Author
        /// </summary>
        /// <param name="author"></param>
        /// <returns>A tuple with errors and a int? that store if sucessful the created id</returns>
        public Tuple<Author, AuthorValidation> CreateAuthor(Author author)
        {
            AuthorValidation validation = new AuthorValidation(author);
            if (validation.IsValid)
            {
                AUTHOR repoAUTHOR = _Repo.CreateAuthor(Mapper.Map<Author, AUTHOR>(author));
                if (repoAUTHOR == null)
                {
                    validation.FailedToCreateAuthor(nameof(author.FirstName));
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
        /// Edit author return tuple with the edited object if successful and a errror dict
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        public Tuple<Author, AuthorValidation> EditAuthor(Author author)
        {
            AuthorValidation validation = new AuthorValidation(author);
            if (validation.IsValid)
            {
                AUTHOR repoAUTHOR = _Repo.EditAuthor(Mapper.Map<Author, AUTHOR>(author));
                if (repoAUTHOR != null)
                {
                    Author editedAuthor = Mapper.Map<AUTHOR, Author>(repoAUTHOR);
                    if (editedAuthor != null)
                        return new Tuple<Author, AuthorValidation>(editedAuthor, validation);
                }
                validation.DoesAlreadyExistOnServer(nameof(author.FirstName));
            }
            return new Tuple<Author, AuthorValidation>(null, validation);
        }

        public bool DeleteAuthor(Author author)
        {
            return _Repo.DeleteAuthor(Mapper.Map<Author, AUTHOR>(author));
        }

        public Search GetAllAuthors(int page, int itemsPerPage)
        {
            Search searchResult = new Search() { AuthorSearchResult = _Repo.GetAllAuthorsFromDB(page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>() };
            return searchResult;
        }

        public List<Author> GetAllAuthorsToList()
        {
            return Mapper.Map<List<AUTHOR>, List<Author>>(_Repo.GetAllAuthorsFromDBToList());
        }

        public Author GetAuthorDetails(int id, int bookPage)
        {
            Author author = Mapper.Map<AUTHOR, Author>(_Repo.GetAuthorDetailsFromDB(id));
            author.BookList = _Repo.GetBooksByAuthor(id, bookPage).ToMappedPagedList<BOOK, Book>();
            BookManager bookManager = new BookManager();
            bookManager.SetupBooks(author.BookList);
            return author;
        }

        public Search GetAuthorsFromSearch(string search, int page, int itemsPerPage)
        {
            Search searchResult = new Search() { AuthorSearchResult = _Repo.GetAuthorsFromSearchResult(search, page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>(), SearchQuery = search };
            return searchResult;
        }

        public Search GetAuthorsFromSearch(Search search, int page, int itemsPerPage)
        {
            search.AuthorSearchResult = _Repo.GetAuthorsFromSearchResult(search.SearchQuery, page, itemsPerPage).ToMappedPagedList<AUTHOR, Author>();
            return search;
        }

        public Author GetAuthorFromID(int id)
        {
            return Mapper.Map<AUTHOR, Author>(_Repo.GetAuthorFromDB(id));
        }
    }
}