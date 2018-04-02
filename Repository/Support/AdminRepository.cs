using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Repository.Support
{
    public class AdminRepository
    {

        public int GetPermissionLevel(string username)
        {
            using (DbLibrary db = new DbLibrary())
            {
                return db.ADMINS.FirstOrDefault(x => x.Username == username).PermissionLevel;
            }
        }

        public ADMIN GetAdmin(string username)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    string query = "SELECT TOP 1 * FROM ADMINS WHERE ADMINS.Username = @user";
                    return db.Database.SqlQuery<ADMIN>(query, new SqlParameter("@user", username)).SingleOrDefault();
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool CreateAdmin(ADMIN admin)
        {
            using (DbLibrary db = new DbLibrary())
            {
                string query = @"INSERT INTO ADMINS VALUES (@Username, @Salt, @PasswordHash, @PermissionLevel, @CanEditClassifications);";
                try
                {
                    db.Database.ExecuteSqlCommand(query,
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

        public bool DeleteAdmin(string username)
        {
            using (DbLibrary db = new DbLibrary())
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
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    string query = "SELECT * FROM ADMINS ORDER BY PermissionLevel DESC, Username";
                    return db.Database.SqlQuery<ADMIN>(query).ToList();
                }
                catch
                {
                    return null;
                }
            }
        }

        public bool EditAdmin(ADMIN eAdmin)
        {
            using (DbLibrary db = new DbLibrary())
            {
                try
                {
                    db.Entry(db.ADMINS.FirstOrDefault(x => x.Username.Equals(eAdmin.Username))).CurrentValues.SetValues(eAdmin);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}