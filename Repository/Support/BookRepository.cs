using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using PagedList;


namespace Repository.Support
{
    public class BookRepository
    {

        public static IPagedList<BOOK> GetAllBooksFromDB(int page, int itemsPerPage)
        {
            using (var db = new dbGrupp3())
            {
                return db.BOOKs.OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
            }
        }

        public static BOOK GetBookFromIsbn(string isbn)
        {
            using(var db = new dbGrupp3())
            {
                return db.BOOKs.Find(isbn);
            }
        }

        public static BOOK EditBook(BOOK eBook)
        {
            using (var db = new dbGrupp3())
            {
                BOOK book = db.BOOKs.Find(eBook.ISBN);
                db.Entry(book).CurrentValues.SetValues(eBook);
                db.SaveChanges();
                return book;
            }
        }

        public static BOOK CreateBook(BOOK book)
        {
            using (var db = new dbGrupp3())
            {
                db.BOOKs.Add(book);
                db.SaveChanges();
                return book;
            }
        }

        public static bool DeleteBook(BOOK ebook)
        {
            using (var db = new dbGrupp3())
            {
                var book = db.BOOKs.Include(a => a.AUTHORs).FirstOrDefault(a => a.ISBN == ebook.ISBN);
                book.AUTHORs.Clear();
                db.BOOKs.Remove(book);
                db.SaveChanges();
                return true;
            }
        }

        public static List<AUTHOR> GetAuthorsFromIsbn(string isbn)
        {
            using (var db = new dbGrupp3())
            {
                return db.Database.SqlQuery<AUTHOR>(
                     @"SELECT dbo.AUTHOR.FirstName, dbo.AUTHOR.LastName , dbo.AUTHOR.BirthYear , dbo.AUTHOR.Aid 
                    FROM ( dbo.AUTHOR INNER JOIN BOOK_AUTHOR ON AUTHOR.Aid = BOOK_AUTHOR.Aid AND BOOK_AUTHOR.ISBN = @isbn)",
                     new SqlParameter("@isbn", isbn)).ToList();

            }
        }

        public static IPagedList<BOOK> GetBookSearchResultat(string search, int page, int itemsPerPage, params int[] classification)
        {
            string classificationString = "";
            List<SqlParameter> classParameters = new List<SqlParameter>();

            if (classification!= null && classification.Count() > 0)
            {
                var lastItem = classification.Last();
                classificationString += " AND (";
                foreach (var item in classification)
                {
                    classParameters.Add(new SqlParameter("@" +item.ToString(), item.ToString()));
                    classificationString += "CLASSIFICATION.SignId = " + classParameters.Last().ParameterName;
                    if (!lastItem.Equals(item))
                    {
                        classificationString += " OR ";
                    }
                }
                classificationString += ");";
            }

            using (var db = new dbGrupp3())
            {
                if (classParameters.Count > 0)
                {
                    classParameters.Add(new SqlParameter("@SEARCH", "%" + search + "%"));
                    return db.Database.SqlQuery<BOOK>(
                    @"SELECT DISTINCT BOOK.ISBN, BOOK.pages, BOOK.publicationinfo, BOOK.PublicationYear, BOOK.SignId, BOOK.Title
                      FROM BOOK INNER JOIN BOOK_AUTHOR ON BOOK.ISBN = BOOK_AUTHOR.ISBN INNER JOIN AUTHOR ON AUTHOR.Aid = BOOK_AUTHOR.Aid INNER JOIN CLASSIFICATION ON CLASSIFICATION.SignId = BOOK.SignId
                      WHERE (BOOK.Title LIKE @SEARCH
                      OR AUTHOR.FirstName LIKE @SEARCH
                      OR AUTHOR.LastName LIKE @SEARCH
                      OR AUTHOR.FirstName + ' ' + AUTHOR.LastName LIKE @SEARCH)" + classificationString
                    , classParameters.ToArray()).ToList().ToPagedList(page, itemsPerPage);
                }
                else
                {
                    return db.Database.SqlQuery<BOOK>(
                    @"SELECT DISTINCT BOOK.ISBN, BOOK.pages, BOOK.publicationinfo, BOOK.PublicationYear, BOOK.SignId, BOOK.Title
                      FROM BOOK JOIN BOOK_AUTHOR ON BOOK.ISBN = BOOK_AUTHOR.ISBN JOIN AUTHOR ON AUTHOR.Aid = BOOK_AUTHOR.Aid
                      WHERE (BOOK.Title LIKE @SEARCH
                      OR AUTHOR.FirstName LIKE @SEARCH
                      OR AUTHOR.LastName LIKE @SEARCH
                      OR AUTHOR.FirstName + ' ' + AUTHOR.LastName LIKE @SEARCH);"
                    , new SqlParameter("@SEARCH", "%" + search + "%")).ToList().ToPagedList(page, itemsPerPage);
                }
            }
        }

    }
}