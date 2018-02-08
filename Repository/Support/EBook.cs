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
                //return db.Database.SqlQuery<BOOK>("SELECT * FROM dbo.BOOK ORDER BY Title").ToList();
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

        public static List<BOOK> GetBookSearchResultat(string search, params string[] classification)
        {
            using(var db = new dbGrupp3())
            {
               return db.Database.SqlQuery<BOOK>(
                    @"SELECT BOOK.ISBN, BOOK.pages, BOOK.publicationinfo, BOOK.PublicationYear, BOOK.SignId, BOOK.Title
                      FROM BOOK JOIN BOOK_AUTHOR ON BOOK.ISBN = BOOK_AUTHOR.ISBN JOIN AUTHOR ON AUTHOR.Aid = BOOK_AUTHOR.Aid
                      WHERE BOOK.Title LIKE '%@search%'
                      OR AUTHOR.FirstName LIKE '%@search%'
                      OR AUTHOR.LastName LIKE '%@search%';
                    "
                    , new SqlParameter("@search", search)).ToList();
            }
        }
    }
}