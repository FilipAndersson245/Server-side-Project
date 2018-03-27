using PagedList;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Support
{
    public class BookRepository
    {
        public bool DoesBookExist(string isbn)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.BOOKs.Any(x => x.ISBN == isbn);
                }
                catch
                {
                    return false;
                }
            }
        }

        public IPagedList<BOOK> GetAllBooksFromDB(int page, int itemsPerPage)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return (from p in db.BOOKs.Include(b => b.AUTHORs).Include(c => c.CLASSIFICATION)
                            orderby p.Title
                            select p)
                            .ToPagedList(page, itemsPerPage);
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<BOOK> GetSearchedBooksFromDBToList(string search)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.BOOKs.Include(b => b.AUTHORs).Where(x => x.Title.Contains(search) || x.ISBN.Contains(search) || x.AUTHORs.Any(y => (y.FirstName + y.LastName)
                        .Contains(search))).OrderBy(x => x.Title).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public BOOK GetBookFromIsbn(string isbn)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.BOOKs.Include(b => b.AUTHORs).Include(b => b.CLASSIFICATION).FirstOrDefault(x => x.ISBN.Equals(isbn));
                }
                catch
                {
                    return null;
                }
            }
        }

        public BOOK EditBook(BOOK eBook)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    BOOK book = db.BOOKs.Include(b => b.AUTHORs).FirstOrDefault(x => x.ISBN.Equals(eBook.ISBN));
                    book.AUTHORs.Clear();
                    db.SaveChanges();
                }
                catch
                {
                    return null;
                }
            }
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    BOOK book = db.BOOKs.Include(b => b.CLASSIFICATION).FirstOrDefault(x => x.ISBN.Equals(eBook.ISBN));
                    db.Entry(book).CurrentValues.SetValues(eBook);
                    db.ChangeTracker.Entries<CLASSIFICATION>().ToList().ForEach(a => a.State = EntityState.Unchanged);
                    book.AUTHORs = new List<AUTHOR>();
                    foreach (var author in eBook.AUTHORs)
                    {
                        db.AUTHORs.Attach(author);
                        book.AUTHORs.Add(author);
                    }
                    db.SaveChanges();
                    return book;
                }
                catch
                {
                    return null;
                }
            }
        }

        public BOOK CreateBook(BOOK book)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.BOOKs.Add(book);
                    db.ChangeTracker.Entries<AUTHOR>().ToList().ForEach(a => a.State = EntityState.Unchanged);
                    db.ChangeTracker.Entries<CLASSIFICATION>().ToList().ForEach(a => a.State = EntityState.Unchanged);
                    db.SaveChanges();
                    return book;
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool DeleteBook(BOOK ebook)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    var book = db.BOOKs.Include(a => a.AUTHORs).FirstOrDefault(a => a.ISBN == ebook.ISBN);
                    book.AUTHORs.Clear();
                    db.BOOKs.Remove(book);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public CLASSIFICATION GetClassificationFromIsbn(BOOK book)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.CLASSIFICATIONs.FirstOrDefault(a => a.SignId == book.SignId);
                }
                catch
                {
                    return null;
                }
            }
        }

        public IPagedList<BOOK> GetBookSearchResultat(string search, int page, int itemsPerPage, params int[] classifications)
        {
            if (classifications != null)
            {
                using (DbLibrary db = new DbLibrary())
                {
                    try
                    {
                        db.Configuration.LazyLoadingEnabled = false;
                        return db.BOOKs.Include(b => b.AUTHORs).Where(b => b.SignId.HasValue && classifications.ToList().Contains(b.SignId.Value))
                            .Where(x => x.Title.Contains(search) || x.ISBN.Contains(search) || x.AUTHORs.Any(y => (y.FirstName + " " + y.LastName).Contains(search)))
                            .OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
            else
            {
                using (DbLibrary db = new DbLibrary())
                {
                    try
                    {
                        db.Configuration.LazyLoadingEnabled = false;
                        return db.BOOKs.Include(b => b.AUTHORs).Where(x => x.Title.Contains(search) || x.ISBN.Contains(search) || x.AUTHORs.Any(y => (y.FirstName + " " + y.LastName).Contains(search)))
                            .OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
                    }
                    catch
                    {
                        return null;
                    }
                }
            }
        }
    }
}