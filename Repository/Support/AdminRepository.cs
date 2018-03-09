using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Repository.Support
{
    public class AdminRepository
    {
        public bool DoesAdminExist(string username)
        {
            using (var db = new dbGrupp3())
            {
                string sql = "SELECT TOP 1 * FROM ADMINS WHERE ADMINS.Username = @user LIMIT 1";
                var existingAdmin = db.Database.SqlQuery<ADMIN>(sql, new SqlParameter("@user", username)).SingleOrDefault();
                return existingAdmin != null;

                //return db.ADMINS.Any(x => x.Username.Equals(username));
            }
        }

        public ADMIN GetAdmin(string username)
        {
            using (var db = new dbGrupp3())
            {
                string sql = "SELECT TOP 1 * FROM ADMINS WHERE ADMINS.Username = @user";
                return db.Database.SqlQuery<ADMIN>(sql, new SqlParameter("@user", username)).SingleOrDefault();

                //return db.ADMINS.FirstOrDefault(x => x.Username.Equals(username));
            }
        }

        public bool CreateAdmin(ADMIN admin)
        {
            using (var db = new dbGrupp3())
            {
                string sql = @"INSERT INTO ADMINS VALUES (@username, @salt, @passwordHash, @ permissionLevel);"; //todo add CanEditClassification to create query

                try
                {
                    db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@username", admin.Username),
                        new SqlParameter("@salt", admin.Salt),
                        new SqlParameter("@passwordHash", admin.PasswordHash),
                        new SqlParameter("@permissionLevel", admin.PermissionLevel));
                    return true;
                }
                catch
                {
                    return false;
                }
                //db.ADMINS.Add(admin);
                //db.SaveChanges();
                //return true;
            }
        }

        public bool resetPassword(string username, string passwordHash, string salt)
        {
            using (var db = new dbGrupp3())
            {
                try
                {
                    string sql = @"UPDATE ADMINS SET ADMINS.PasswordHash @passwordHash, ADMINS.Salt = @salt WHERE ADMINS.Username = @username";
                    db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@username", username),
                        new SqlParameter("@passwordHash", passwordHash),
                        new SqlParameter("@salt", salt));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<ADMIN> GetAllAdmins()
        {
            using (var db = new dbGrupp3())
            {
                string sql = "SELECT * FROM ADMINS ORDER BY Username";
                return db.Database.SqlQuery<ADMIN>(sql).ToList();
                //return db.ADMINS.OrderBy(x => x.Username).ToList();
            }
        }
    }
}