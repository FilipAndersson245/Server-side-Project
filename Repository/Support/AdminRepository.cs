using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Repository.Support
{
    public class AdminRepository
    {
        public static bool DoesAdminExist(string username)
        {
            using (var db = new dbGrupp3())
            {
                return db.ADMINS.Any(x => x.Username.Equals(username));
            }
        }

        public static ADMIN GetAdmin(string username)
        {
            using(var db = new dbGrupp3())
            {
                return db.ADMINS.FirstOrDefault(x => x.Username.Equals(username));
            }
        }

        public static bool CreateAdmin(ADMIN admin)
        {
            using (var db = new dbGrupp3())
            {
                db.ADMINS.Add(admin);
                db.SaveChanges();
                return true;
            }
        }

        public static List<ADMIN> GetAllAdmins()
        {
            using (var db = new dbGrupp3())
            {
                return db.ADMINS.OrderBy(x => x.Username).ToList();
            }
        }
    }
}