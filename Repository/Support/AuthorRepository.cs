using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;
using PagedList;

namespace Repository.Support
{
    public class AuthorRepository
    {

        public static IPagedList<AUTHOR> GetAllAuthorsFromDB(int page, int itemsPerPage)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.OrderBy(x => x.LastName).ToPagedList(page, itemsPerPage);
            }
        }

        public static AUTHOR GetAuthorDetailsFromDB(int id)
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

        public static bool DeleteAuthor(AUTHOR eauthor) //Returns true if amount of SaveChanges (int) is bigger than 1
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

        public static AUTHOR EditAuthor(AUTHOR eauthor) //Returns the updated author
        {
            using (var db = new dbGrupp3())
            {
                AUTHOR updatedAUTHOR = db.AUTHORs.Find(eauthor.Aid);
                db.Entry(updatedAUTHOR).CurrentValues.SetValues(eauthor);
                db.SaveChanges();
                return updatedAUTHOR;
            }
        }

        public static IPagedList<BOOK> GetBooksByAuthor(int id, int page)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.Find(id).BOOKs.ToPagedList(page, 100);
            }
        }

        public static IPagedList<AUTHOR> GetAuthorsFromSearchResult(string search, int page, int itemsPerPage)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.Where(x  => ((x.FirstName + " " + x.LastName).Contains(search))).OrderBy(x => x.LastName).ToPagedList(page,itemsPerPage);
                
            }
        }

        public static AUTHOR GetAuthorFromDB(int id)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.Find(id);
            }
        }

    }

}