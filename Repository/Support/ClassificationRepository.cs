using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Repository.Support
{
    public class ClassificationRepository
    {
        public bool DoesClassificationExist(int signId)
        {
            using (var db = new dbLibrary())
            {
                return db.CLASSIFICATIONs.Any(x => x.SignId.Equals(signId));
            }
        }

        public bool DoesClassificationExist(string signum)
        {
            using (var db = new dbLibrary())
            {
                return db.CLASSIFICATIONs.Any(x => x.Signum.Equals(signum));
            }
        }

        public CLASSIFICATION GetClassificationForBook(string isbn)
        {
            BookRepository repo = new BookRepository();
            return repo.GetBookFromIsbn(isbn).CLASSIFICATION;
        }

        public List<BOOK> GetBooksFromClassification(int signId)
        {
            using (var db = new dbLibrary())
            {
                return db.CLASSIFICATIONs.Find(signId).BOOKs.ToList();
            }
        }

        public List<CLASSIFICATION> GetAllClassifications()
        {
            using (var db = new dbLibrary())
            {
                return db.CLASSIFICATIONs.OrderBy(x => x.Signum).ToList();
            }
        }

        public int GetNewID()
        {
            using (var db = new dbLibrary())
            {
                return db.CLASSIFICATIONs.Max(a => a.SignId) + 1;
            }
        }

        public bool CreateClassification(CLASSIFICATION eClassification)
        {
            try
            {
                using (var db = new dbLibrary())
                {
                    eClassification.SignId = GetNewID();
                    db.CLASSIFICATIONs.Add(eClassification);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public CLASSIFICATION GetClassificationFromID(int id)
        {
            using (var db = new dbLibrary())
            {
                return db.CLASSIFICATIONs.FirstOrDefault(x => x.SignId.Equals(id));
            }
        }

        public CLASSIFICATION GetClassificationFromName(string signum)
        {
            using (var db = new dbLibrary())
            {
                return db.CLASSIFICATIONs.FirstOrDefault(a => a.Signum == signum);
            }
        }

        public bool DeleteClassification(CLASSIFICATION eClassification)
        {
            try
            {
                using (var db = new dbLibrary())
                {
                    var classification = db.CLASSIFICATIONs.Include(a => a.BOOKs).FirstOrDefault(a => a.SignId == eClassification.SignId);
                    classification.BOOKs.Clear();
                    db.CLASSIFICATIONs.Remove(classification);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EditClassification(CLASSIFICATION eClassification)
        {
            try
            {
                using (var db = new dbLibrary())
                {
                    CLASSIFICATION classification = db.CLASSIFICATIONs.FirstOrDefault(x => x.SignId.Equals(eClassification.SignId));
                    db.Entry(classification).CurrentValues.SetValues(eClassification);
                    db.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool DoesClassificationContainBooks(CLASSIFICATION classification)
        {
            using (var db = new dbLibrary())
            {
                return (db.CLASSIFICATIONs.Find(classification.SignId).BOOKs.ToList().Count() > 0);
            }
        }
    }
}