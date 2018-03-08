using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.SqlClient;

namespace Repository.Support
{
    public class AdminRepository
    {
        public bool DoesAdminExist(string username)
        {
            using (var db = new dbGrupp3())
            {
                string sql = "SELECT TOP 1 * FROM ADMIN WHERE ADMIN.Username = @user LIMIT 1";
                var existingAdmin =  db.Database.SqlQuery<ADMIN>(sql, new SqlParameter("@user", username)).FirstOrDefault();
                return existingAdmin != null;

                //return db.ADMINS.Any(x => x.Username.Equals(username));
            }
        }

        public ADMIN GetAdmin(string username)
        {
            using(var db = new dbGrupp3())
            {
                string sql = "SELECT * FROM ADMIN WHERE Username = @user";
                return db.Database.SqlQuery<ADMIN>(sql, new SqlParameter("@user", username)).FirstOrDefault();

                //return db.ADMINS.FirstOrDefault(x => x.Username.Equals(username));
            }
        }

        public bool CreateAdmin(ADMIN admin)
        {
            using (var db = new dbGrupp3())
            {
                db.ADMINS.Add(admin);
                db.SaveChanges();
                return true;
            }
        }

        public List<ADMIN> GetAllAdmins()
        {
            using (var db = new dbGrupp3())
            {
                string sql = "SELECT * FROM ADMIN ORDER BY Username";
                return db.Database.SqlQuery<ADMIN>(sql).ToList();
                //return db.ADMINS.OrderBy(x => x.Username).ToList();
            }
        }
    }
}