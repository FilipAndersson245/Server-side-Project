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
    }
}