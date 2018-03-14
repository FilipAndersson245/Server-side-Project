using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Repository.Support
{
    public class AdminRepository
    {
        public bool DoesAdminExist(string username)
        {
            using (var db = new dbLibrary())
            {
                string sql = "SELECT TOP 1 * FROM ADMINS WHERE ADMINS.Username = @user LIMIT 1";
                return db.Database.SqlQuery<ADMIN>(sql, new SqlParameter("@user", username)).SingleOrDefault() != null;
            }
        }

        public int GetPermissionLevel(string username)
        {
            using (var db = new dbLibrary())
            {
                return db.ADMINS.FirstOrDefault(x => x.Username == username).PermissionLevel;
            }
        }

        public ADMIN GetAdmin(string username)
        {
            using (var db = new dbLibrary())
            {
                string sql = "SELECT TOP 1 * FROM ADMINS WHERE ADMINS.Username = @user";
                return db.Database.SqlQuery<ADMIN>(sql, new SqlParameter("@user", username)).SingleOrDefault();
            }
        }

        public bool CreateAdmin(ADMIN admin)
        {
            using (var db = new dbLibrary())
            {
                string sql = @"INSERT INTO ADMINS VALUES (@Username, @Salt, @PasswordHash, @PermissionLevel, @CanEditClassifications);"; //todo add CanEditClassification to create query
                try
                {
                    db.Database.ExecuteSqlCommand(sql,
                        new SqlParameter("@Username", admin.Username),
                        new SqlParameter("@Salt", admin.Salt),
                        new SqlParameter("@PasswordHash", admin.PasswordHash),
                        new SqlParameter("@PermissionLevel", admin.PermissionLevel),
                        new SqlParameter("@CanEditClassifications", admin.CanEditClassifications));
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool ResetPassword(string username, string passwordHash, string salt)
        {
            using (var db = new dbLibrary())
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

        public bool DeleteAdmin(string username)
        {
            using (var db = new dbLibrary())
            {
                try
                {
                    var admin = db.ADMINS.SingleOrDefault(x => x.Username.Equals(username));
                    if (admin != null)
                    {
                        db.ADMINS.Remove(admin);
                        db.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch
                {
                    return false;
                }
            }
        }

        public List<ADMIN> GetAllAdmins()
        {
            using (var db = new dbLibrary())
            {
                string sql = "SELECT * FROM ADMINS ORDER BY Username";
                return db.Database.SqlQuery<ADMIN>(sql).ToList();
            }
        }
    }
}