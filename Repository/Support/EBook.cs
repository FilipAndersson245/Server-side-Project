using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Repository.Support
{
    public class EBook
    {

        public static List<BOOK> getAllBooksFromDB()
        {
            using (var db = new dbGrupp3())
            {
                return db.Database.SqlQuery<BOOK>("SELECT * FROM dbo.BOOK ORDER BY Title", new SqlParameter("bajs", 1)).ToList();
            }
        }


        public static List<AUTHOR> GetAuthorsFromIsbn(string isbn)
        {
            using (var db = new dbGrupp3())
            {
                //string isbn = "0070062722";
                return db.Database.SqlQuery<AUTHOR>(
                     @"SELECT dbo.AUTHOR.FirstName, dbo.AUTHOR.LastName , dbo.AUTHOR.BirthYear , dbo.AUTHOR.Aid 
                    FROM ( dbo.AUTHOR INNER JOIN BOOK_AUTHOR ON AUTHOR.Aid = BOOK_AUTHOR.Aid AND BOOK_AUTHOR.ISBN = @isbn)",
                     new SqlParameter("@isbn", isbn)).ToList();

            }
        }
    }
}