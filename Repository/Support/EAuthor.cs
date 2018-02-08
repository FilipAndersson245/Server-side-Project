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

        public static AUTHOR getAuthorDetailsFromDB(string id)
        {
            using (var db = new dbGrupp3())
            {
                return db.AUTHORs.Find(id);
            }
        }

    }

}