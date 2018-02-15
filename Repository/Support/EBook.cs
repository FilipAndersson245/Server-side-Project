using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;


namespace Repository.Support
{
    public class EBook
    {

        public static List<BOOK> getAllBooksFromDB()
        {
            using (var db = new dbGrupp3())
            {
                return db.BOOKs.OrderBy(x => x.Title).ToList();
            }
        }

        public static BOOK getBookFromIsbn(string isbn)
        {
            using(var db = new dbGrupp3())
            {
                return db.BOOKs.Find(isbn);
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

        //may not work!!!
        public static bool updateBook(BOOK updatedBook, string oldIsbn)
        {
            using (var db = new dbGrupp3())
            {
                if(db.BOOKs.Find(updatedBook.ISBN) == null)
                {
                    var book = db.BOOKs.Find(oldIsbn);
                    if (book != null)
                    {
                        book = updatedBook;
                        db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }

        public static List<BOOK> GetBookSearchResultat(string search, params int[] classification)
        {
            string classificationString = "";
            List<SqlParameter> classParameters = new List<SqlParameter>();
            if (classification.Count() > 0)
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
                    , classParameters.ToArray()).ToList();
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
                    , new SqlParameter("@SEARCH", "%" + search + "%")).ToList();
                }
            }
        }

    }
}