using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class EClassification
    {
        static public CLASSIFICATION GetClassificationForBook(string isbn)
        {
            return EBook.getBookFromIsbn(isbn).CLASSIFICATION;
        }

        static public List<BOOK> GetBooksFromClassification(int signId)
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.Find(signId).BOOKs.ToList();
            }
        }
        static public List<CLASSIFICATION> GetAllClassifications()
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.ToList();
            }
        }

        public static int getNewID()
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.Max(a => a.SignId) + 1;
            }
        }

        public static bool createClassification(CLASSIFICATION eClassification)
        {
            using (var db = new dbGrupp3())
            {
                eClassification.SignId = EClassification.getNewID();
                db.CLASSIFICATIONs.Add(eClassification);
                db.SaveChanges();
                return true;
            }
        }

        public static CLASSIFICATION getClassificationFromID(int id)
        {
            using (var db = new dbGrupp3())
            {
                return db.CLASSIFICATIONs.Find(id);
            }
        }

        public static bool deleteClassification(CLASSIFICATION eClassification)
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

        public static bool editClassification(CLASSIFICATION eClassification)
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
