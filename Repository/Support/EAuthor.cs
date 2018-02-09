using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Repository.Support
{
    public class EAuthor
    {

        public static List<AUTHOR> getAllAuthorsFromDB()
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.OrderBy(x => x.LastName).ToList();
            }
        }

        public static AUTHOR getAuthorDetailsFromDB(int id)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.Find(id);
            }
        }

        public static List<AUTHOR> getAuthorsFromSearchResultat(string search)
        {
            using (var db = new dbGrupp3())
            {
                return db.Database.SqlQuery<AUTHOR>(@"SELECT AUTHOR.Aid,AUTHOR.FirstName,AUTHOR.LastName,AUTHOR.BirthYear
                                                      FROM AUTHOR
                                                      WHERE AUTHOR.FirstName LIKE @SEARCH
                                                      OR AUTHOR.LastName LIKE @SEARCH
                                                      OR AUTHOR.FirstName + ' ' + AUTHOR.LastName LIKE @SEARCH",
                                               new SqlParameter("@SEARCH", "%" + search + " % ")).ToList();
            }
        }

    }

}