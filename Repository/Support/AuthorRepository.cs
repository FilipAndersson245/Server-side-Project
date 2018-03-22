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
            using (DbLibrary db = new DbLibrary())
            {
                return db.AUTHORs.Any(x => x.Aid.Equals(aid));
            }
        }

        public IPagedList<AUTHOR> GetAllAuthorsFromDB(int page, int itemsPerPage)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.AUTHORs.OrderBy(x => x.LastName).ToPagedList(page, itemsPerPage);
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<AUTHOR> GetAllAuthorsFromDBToList()
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.AUTHORs.OrderBy(x => x.LastName).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public AUTHOR GetAuthorDetailsFromDB(int id)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.AUTHORs.FirstOrDefault(x => x.Aid.Equals(id));
                }
                catch
                {
                    return null;
                }
            }
        }

        public AUTHOR CreateAuthor(AUTHOR author) //Returns Aid if successfull, 0 if failed
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.AUTHORs.Add(author);
                    db.SaveChanges();
                    return author;
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool DeleteAuthor(AUTHOR eauthor) //Returns true if amount of SaveChanges (int) is bigger than 1
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    var author = db.AUTHORs.Include(a => a.BOOKs).FirstOrDefault(a => a.Aid.Equals(eauthor.Aid));
                    author.BOOKs.Clear();
                    db.AUTHORs.Remove(author);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public AUTHOR EditAuthor(AUTHOR eauthor) //Returns the updated author and if failed returns null
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    AUTHOR updatedAUTHOR = db.AUTHORs.Find(eauthor.Aid);
                    db.Entry(updatedAUTHOR).CurrentValues.SetValues(eauthor);
                    db.SaveChanges();
                    return updatedAUTHOR;
                }
                catch
                {
                    return null;
                }
            }
        }

        public IPagedList<BOOK> GetBooksByAuthor(int id, int page)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.AUTHORs.Include(a => a.BOOKs).First(a => a.Aid.Equals(id)).BOOKs.ToPagedList(page, 100);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IPagedList<AUTHOR> GetAuthorsFromSearchResult(string search, int page, int itemsPerPage)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.AUTHORs.Where(x => ((x.FirstName + " " + x.LastName).Contains(search))).OrderBy(x => x.LastName).ToPagedList(page, itemsPerPage);
                }
                catch
                {
                    return null;
                }
            }
        }

        public AUTHOR GetAuthorFromDB(int id)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.AUTHORs.Find(id);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}