using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class EClassification
    {
        static public List<CLASSIFICATION> GetClassificationForBook(string isbn)
        {
            return EBook.getBookFromIsbn(isbn).CLASSIFICATIONs
            throw new NotImplementedException();
        }
    }
}