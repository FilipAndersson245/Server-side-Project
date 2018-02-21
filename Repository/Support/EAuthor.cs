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

        public static int CreateAuthor(AUTHOR author) //Returns Aid if successfull, 0 if failed
        {
            using (var db = new dbGrupp3())
            {
                db.AUTHORs.Add(author);
                db.SaveChanges();
                return author.Aid;
            }
        }

        public static bool deleteAuthor(AUTHOR eauthor) //Returns true if amount of SaveChanges (int) is bigger than 1
        {
            using (var db = new dbGrupp3())
            {
                var author = db.AUTHORs.Include(a => a.BOOKs).FirstOrDefault(a => a.Aid == eauthor.Aid);
                author.BOOKs.Clear();
                db.AUTHORs.Remove(author);
                db.SaveChanges();
                return true;
            }
        }

        public static AUTHOR updateAuthor(AUTHOR eauthor) //Returns the updated author
        {
            using (var db = new dbGrupp3())
            {
                AUTHOR updatedAUTHOR = db.AUTHORs.Find(eauthor.Aid);
                db.Entry(updatedAUTHOR).CurrentValues.SetValues(eauthor);
                db.SaveChanges();
                return updatedAUTHOR;
            }
        }

        public static IPagedList<BOOK> getBooksByAuthor(int id, int page)
        {
            using (var db = new dbGrupp3())
            {
                return db.Database.SqlQuery<BOOK>(@"SELECT dbo.BOOK.ISBN, dbo.BOOK.pages,
                                                    dbo.BOOK.publicationinfo, dbo.BOOK.PublicationYear,
                                                    dbo.BOOK.SignId, dbo.BOOK.Title FROM
                                                    (dbo.BOOK INNER JOIN BOOK_AUTHOR ON BOOK.ISBN = BOOK_AUTHOR.ISBN
                                                    AND BOOK_AUTHOR.Aid = @authorID)",
                                                    new SqlParameter("@authorID", id)).ToList().ToPagedList(page, 100);
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

        public static AUTHOR getAuthorFromDB(int id)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.Find(id);
            }
        }

    }

}