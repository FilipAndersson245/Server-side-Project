using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Support
{
    public class AuthorRepository
    {
        public bool DoesAuthorExist(int aid)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.AUTHORs.Any(x => x.Aid.Equals(aid));
            }
        }

        public IPagedList<AUTHOR> GetAllAuthorsFromDB(int page, int itemsPerPage)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.AUTHORs.OrderBy(x => x.LastName).ToPagedList(page, itemsPerPage);
            }
        }

        public List<AUTHOR> GetAllAuthorsFromDBToList()
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.AUTHORs.OrderBy(x => x.LastName).ToList();
            }
        }

        public AUTHOR GetAuthorDetailsFromDB(int id)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.AUTHORs.FirstOrDefault(x => x.Aid.Equals(id));
            }
        }

        public AUTHOR CreateAuthor(AUTHOR author) //Returns Aid if successfull, 0 if failed
        {
            try
            {
                using (dbLibrary db = new dbLibrary())
                {
                    db.AUTHORs.Add(author);
                    db.SaveChanges();
                    return author;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool DeleteAuthor(AUTHOR eauthor) //Returns true if amount of SaveChanges (int) is bigger than 1
        {
            try
            {
                using (dbLibrary db = new dbLibrary())
                {
                    var author = db.AUTHORs.Include(a => a.BOOKs).FirstOrDefault(a => a.Aid.Equals(eauthor.Aid));
                    author.BOOKs.Clear();
                    db.AUTHORs.Remove(author);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public AUTHOR EditAuthor(AUTHOR eauthor) //Returns the updated author and if failed returns null
        {
            try
            {
                using (dbLibrary db = new dbLibrary())
                {
                    AUTHOR updatedAUTHOR = db.AUTHORs.Find(eauthor.Aid);
                    db.Entry(updatedAUTHOR).CurrentValues.SetValues(eauthor);
                    db.SaveChanges();
                    return updatedAUTHOR;
                }
            }
            catch
            {
                return null;
            }
        }

        public IPagedList<BOOK> GetBooksByAuthor(int id, int page)
        {
            using (dbLibrary db = new dbLibrary())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var data = db.AUTHORs.Include(a => a.BOOKs).First(a => a.Aid.Equals(id)).BOOKs.ToPagedList(page, 100);
                return data;
            }
        }

        public IPagedList<AUTHOR> GetAuthorsFromSearchResult(string search, int page, int itemsPerPage)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.AUTHORs.Where(x => ((x.FirstName + " " + x.LastName).Contains(search))).OrderBy(x => x.LastName).ToPagedList(page, itemsPerPage);
            }
        }

        public AUTHOR GetAuthorFromDB(int id)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.AUTHORs.Find(id);
            }
        }
    }
}