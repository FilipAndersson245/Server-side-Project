using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Support
{
    public class ClassificationRepository
    {

        public bool DoesClassificationExist(string signum)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.CLASSIFICATIONs.Any(x => x.Signum.Equals(signum));
                }
                catch
                {
                    return false;
                }
            }
        }

        public CLASSIFICATION GetClassificationForBook(string isbn)
        {
            BookRepository repo = new BookRepository();
            return repo.GetBookFromIsbn(isbn).CLASSIFICATION;
        }

        public List<BOOK> GetBooksFromClassification(int signId)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.CLASSIFICATIONs.Find(signId).BOOKs.ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public List<CLASSIFICATION> GetAllClassifications()
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.CLASSIFICATIONs.OrderBy(x => x.Signum).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public int GetNewID()
        {
            using (DbLibrary db = new DbLibrary())
            {
                return db.CLASSIFICATIONs.Max(a => a.SignId) + 1;
            }
        }

        public bool CreateClassification(CLASSIFICATION eClassification)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    eClassification.SignId = GetNewID();
                    db.CLASSIFICATIONs.Add(eClassification);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public CLASSIFICATION GetClassificationFromID(int id)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.CLASSIFICATIONs.FirstOrDefault(x => x.SignId.Equals(id));
                }
                catch
                {
                    return null;
                }
            }
        }

        public CLASSIFICATION GetClassificationFromName(string signum)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return db.CLASSIFICATIONs.FirstOrDefault(a => a.Signum == signum);
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool DeleteClassification(CLASSIFICATION eClassification)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    var classification = db.CLASSIFICATIONs.Include(a => a.BOOKs).FirstOrDefault(a => a.SignId == eClassification.SignId);
                    classification.BOOKs.Clear();
                    db.CLASSIFICATIONs.Remove(classification);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool EditClassification(CLASSIFICATION eClassification)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    CLASSIFICATION classification = db.CLASSIFICATIONs.FirstOrDefault(x => x.SignId.Equals(eClassification.SignId));
                    db.Entry(classification).CurrentValues.SetValues(eClassification);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            
        }

        public bool DoesClassificationContainBooks(CLASSIFICATION classification)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    return (db.CLASSIFICATIONs.Find(classification.SignId).BOOKs.ToList().Count() > 0);
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}