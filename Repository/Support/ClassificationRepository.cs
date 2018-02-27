using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class ClassificationRepository
    {
        public bool DoesClassificationExist(int signId)
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.Any(x => x.SignId.Equals(signId));
            }
        }

        public bool DoesClassificationExist(string name)
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.Any(x => x.Signum.Equals(name));
            }
        }

        public CLASSIFICATION GetClassificationForBook(string isbn)
        {
            BookRepository repo = new BookRepository();
            return repo.GetBookFromIsbn(isbn).CLASSIFICATION;
        }

        public List<BOOK> GetBooksFromClassification(int signId)
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.Find(signId).BOOKs.ToList();
            }
        }

        public List<CLASSIFICATION> GetAllClassifications()
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.OrderBy(x => x.Signum).ToList();
            }
        }

        public int GetNewID()
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.Max(a => a.SignId) + 1;
            }
        }

        public bool CreateClassification(CLASSIFICATION eClassification)
        {
            using (var db = new dbGrupp3())
            {
                eClassification.SignId = GetNewID();
                db.CLASSIFICATIONs.Add(eClassification);
                db.SaveChanges();
                return true;
            }
        }

        public CLASSIFICATION GetClassificationFromID(int id)
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.Find(id);
            }
        }

        public CLASSIFICATION GetClassificationFromName(string name)
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.FirstOrDefault(a => a.Signum == name);
            }
        }

        public bool DeleteClassification(CLASSIFICATION eClassification)
        {
            using (var db = new dbGrupp3())
            {
                var classification = db.CLASSIFICATIONs.Include(a => a.BOOKs).FirstOrDefault(a => a.SignId == eClassification.SignId);
                classification.BOOKs.Clear();
                db.CLASSIFICATIONs.Remove(classification);
                db.SaveChanges();
                return true;
            }
        }

        public bool EditClassification(CLASSIFICATION eClassification)
        {
            using (var db = new dbGrupp3())
            {
                CLASSIFICATION classification = db.CLASSIFICATIONs.Find(eClassification.SignId);
                db.Entry(classification).CurrentValues.SetValues(eClassification);
                db.SaveChanges();
                return true;
            }
        }
    }
}
