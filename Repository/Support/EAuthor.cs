using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using PagedList;

namespace Repository.Support
{
    public class EAuthor
    {

        public static IPagedList<AUTHOR> getAllAuthorsFromDB(int page, int itemsPerPage)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.OrderBy(x => x.LastName).ToPagedList(page, itemsPerPage);
            }
        }

        public static AUTHOR getAuthorDetailsFromDB(int id)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.Find(id);
            }
        }

        public static IPagedList<BOOK> getBooksByAuthor(int id, int page, int itemsPerPage)
        {
            using (var db = new dbGrupp3())
            {
                return db.Database.SqlQuery<BOOK>(@"SELECT dbo.BOOK.ISBN, dbo.BOOK.pages,
                                                    dbo.BOOK.publicationinfo, dbo.BOOK.PublicationYear,
                                                    dbo.BOOK.SignId, dbo.BOOK.Title FROM
                                                    (dbo.BOOK INNER JOIN BOOK_AUTHOR ON BOOK.ISBN = BOOK_AUTHOR.ISBN
                                                    AND BOOK_AUTHOR.Aid = @authorID)",
                                                    new SqlParameter("@authorID", id)).ToList().ToPagedList(page, itemsPerPage);
            }
        }

        public static IPagedList<AUTHOR> getAuthorsFromSearchResult(string search, int page, int itemsPerPage)
        {
            using (var db = new dbGrupp3())
            {
                return db.Database.SqlQuery<AUTHOR>(@"SELECT DISTINCT AUTHOR.Aid,AUTHOR.FirstName,AUTHOR.LastName,AUTHOR.BirthYear
                                                      FROM AUTHOR
                                                      WHERE AUTHOR.FirstName LIKE @SEARCH
                                                      OR AUTHOR.LastName LIKE @SEARCH
                                                      OR AUTHOR.FirstName + ' ' + AUTHOR.LastName LIKE @SEARCH",
                                               new SqlParameter("@SEARCH", "%" + search + "%")).ToList().ToPagedList(page, itemsPerPage);
            }
        }

    }

}