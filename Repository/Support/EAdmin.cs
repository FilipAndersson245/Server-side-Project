using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class EAdmin
    {
        public static ADMIN getAdmin(string username)
        {
            using(var db = new dbGrupp3())
            {
                return db.ADMINS.FirstOrDefault(x => x.Username == username);
            }
        }

        public static bool createAdmin(ADMIN admin)
        {
            using (var db = new dbGrupp3())
            {
                db.ADMINS.Add(admin);
                db.SaveChanges();
                return true;
            }
        }

        public static List<ADMIN> getAllAdmins()
        {
            using (var db = new dbGrupp3())
            {
                return db.ADMINS.OrderBy(x => x.Username).ToList();
            }
        }
    }
}