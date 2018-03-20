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
            using (dbLibrary db = new dbLibrary())
            {
                return db.BOOKs.Any(x => x.ISBN == isbn);
            }
        }

        public IPagedList<BOOK> GetAllBooksFromDB(int page, int itemsPerPage)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.BOOKs.Include(b => b.AUTHORs).Include(b => b.CLASSIFICATION).OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
            }
        }

        public List<BOOK> GetSearchedBooksFromDBToList(string search)
        {
            using (dbLibrary db = new dbLibrary())
            {
                db.Configuration.LazyLoadingEnabled = false;
                return db.BOOKs.Include(b => b.AUTHORs).Where(x => x.Title.Contains(search) || x.ISBN.Contains(search) || x.AUTHORs.Any(y => (y.FirstName + y.LastName)
                    .Contains(search))).OrderBy(x => x.Title).ToList();
            }
        }

        public BOOK GetBookFromIsbn(string isbn)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.BOOKs.Include(b => b.AUTHORs).Include(b => b.CLASSIFICATION).FirstOrDefault(x => x.ISBN.Equals(isbn));
            }
        }

        public BOOK EditBook(BOOK eBook)
        {
            using (dbLibrary db = new dbLibrary())
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
            using (dbLibrary db = new dbLibrary())
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
            using (dbLibrary db = new dbLibrary())
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
            using (dbLibrary db = new dbLibrary())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var book = db.BOOKs.Include(a => a.AUTHORs).FirstOrDefault(a => a.ISBN == ebook.ISBN);
                book.AUTHORs.Clear();
                db.BOOKs.Remove(book);
                db.SaveChanges();
                return true;
            }
        }

        public List<AUTHOR> GetAuthorsFromIsbn(string isbn)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.BOOKs.Find(isbn).AUTHORs.ToList();
            }
        }

        public CLASSIFICATION GetClassificationFromIsbn(BOOK book)
        {
            using (dbLibrary db = new dbLibrary())
            {
                return db.CLASSIFICATIONs.FirstOrDefault(a => a.SignId == book.SignId);
            }
        }

        public IPagedList<BOOK> GetBookSearchResultat(string search, int page, int itemsPerPage, params int[] classifications)
        {
            if (classifications != null)
            {
                using (dbLibrary db = new dbLibrary())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.BOOKs.Include(b => b.AUTHORs).Where(b => b.SignId.HasValue && classifications.ToList().Contains(b.SignId.Value))
                        .Where(x => x.Title.Contains(search) || x.ISBN.Contains(search) || x.AUTHORs.Any(y => (y.FirstName + y.LastName).Contains(search)))
                        .OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
                }
            }
            else
            {
                using (dbLibrary db = new dbLibrary())
                {
                    db.Configuration.LazyLoadingEnabled = false;
                    return db.BOOKs.Where(x => x.Title.Contains(search) || x.ISBN.Contains(search) || x.AUTHORs.Any(y => (y.FirstName + y.LastName).Contains(search)))
                        .OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
                }
            }
        }
    }
}