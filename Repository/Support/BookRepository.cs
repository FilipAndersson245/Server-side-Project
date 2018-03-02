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
        public bool DoesBookExist(string isbn)
        {
            using (var db = new dbGrupp3())
            {
                return db.BOOKs.Any(x => x.ISBN == isbn);
            }
        }

        public IPagedList<BOOK> GetAllBooksFromDB(int page, int itemsPerPage)
        {
            using (var db = new dbGrupp3())
            {
                db.Database.Log = s => System.Diagnostics.Debug.Write(s);
                return db.BOOKs.Include(b => b.AUTHORs).Include(b =>b.CLASSIFICATION).OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
            }
        }

        public BOOK GetBookFromIsbn(string isbn)
        {
            using(var db = new dbGrupp3())
            {
                return db.BOOKs.FirstOrDefault(x => x.ISBN.Equals(isbn));
            }
        }

        public BOOK EditBook(BOOK eBook)
        {
            using (var db = new dbGrupp3())
            {
                BOOK book = db.BOOKs.FirstOrDefault(x => x.ISBN.Equals(eBook.ISBN));
                db.Entry(book).CurrentValues.SetValues(eBook);
                db.SaveChanges();
                return book;
            }
        }

        public BOOK CreateBook(BOOK book)
        {
            using (var db = new dbGrupp3())
            {
                db.BOOKs.Add(book);
                db.ChangeTracker.Entries<AUTHOR>().ToList().ForEach(a => a.State = EntityState.Unchanged);
                db.SaveChanges();
                return book;
            }
        }

        public bool DeleteBook(BOOK ebook)
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

        public List<AUTHOR> GetAuthorsFromIsbn(string isbn)
        {
            using (var db = new dbGrupp3())
            {
                return db.BOOKs.Find(isbn).AUTHORs.ToList();
            }
        }

        public CLASSIFICATION GetClassificationFromIsbn(BOOK book)
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.FirstOrDefault(a => a.SignId == book.SignId);
            }
        }

        public IPagedList<BOOK> GetBookSearchResultat(string search, int page, int itemsPerPage, params int[] classifications)
        {
            
            if(classifications != null)
            {
                using (var db = new dbGrupp3())
                {
                    return db.BOOKs.Include(b => b.AUTHORs).Where(b => b.SignId.HasValue && classifications.ToList().Contains(b.SignId.Value))
                        .Where(x => x.Title.Contains(search) || x.ISBN.Contains(search) || x.AUTHORs.Any(y => (y.FirstName + y.LastName).Contains(search)))
                        .OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
                }
            }
            else
            {
                using (var db = new dbGrupp3())
                {
                    return db.BOOKs.Where(x => x.Title.Contains(search) || x.ISBN.Contains(search) || x.AUTHORs.Any(y => (y.FirstName + y.LastName).Contains(search)))
                        .OrderBy(x => x.Title).ToPagedList(page, itemsPerPage);
                }
            }
        }
    }
}