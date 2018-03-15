using AutoMapper;
using Repository;
using Repository.Support;
using Service.Models;
using Service.Tools;
using Service.Validations;
using System.Collections.Generic;

namespace Service.Managers
{
    public class AdminManager
    {
        public AdminValidation Login(Admin admin)
        {
            AdminValidation validation = new AdminValidation(admin);
            if (validation.IsValid)
            {
                Admin dbAdmin = GetAdmin(admin.Username);
                if (dbAdmin == null)
                {
                    validation.DoesNotExistOnServer(nameof(admin.Username));
                }
                else
                {
                    Hashing pwdHash = new Hashing(admin.Password, dbAdmin.Salt);
                    if (pwdHash.Equals(dbAdmin.PasswordHash))
                        return validation;
                    else
                    {
                        validation.WrongPassword(nameof(admin.Password));
                    }
                }
            }
            return validation;
        }

        public Rank getPermissionLevel(string username)
        {
            AdminRepository repo = new AdminRepository();
            return (Rank)repo.GetPermissionLevel(username);
        }

        public AdminValidation SignUp(Admin admin)
        {
            AdminValidation validation = new AdminValidation(admin);
            if (validation.IsValid)
            {
                Admin existingAdmin = GetAdmin(admin.Username);
                if (existingAdmin != null)
                    validation.DoesAlreadyExistOnServer(nameof(admin.Username));
                else
                {
                    Hashing hash = new Hashing(admin.Password);
                    admin.Password = null;
                    admin.PasswordHash = hash.Hash;
                    admin.Salt = hash.Salt;
                    CreateAdmin(admin);
                    return validation;
                }
            }
            return validation;
        }

        public Admin GetAdmin(string username)
        {
            AdminRepository repo = new AdminRepository();
            return Mapper.Map<Admin>(repo.GetAdmin(username));
        }

        public bool CreateAdmin(Admin admin)
        {
            AdminRepository repo = new AdminRepository();
            ADMIN dbAdmin = Mapper.Map<ADMIN>(admin);
            return repo.CreateAdmin(dbAdmin);
        }

        public List<Admin> GetAllAdmins()
        {
            AdminRepository repo = new AdminRepository();
            return Mapper.Map<List<ADMIN>, List<Admin>>(repo.GetAllAdmins());
        }

        public bool DeleteAdmin(string id)
        {
            AdminRepository repo = new AdminRepository();
            return repo.DeleteAdmin(id);
        }

        public AdminValidation EditAdmin(Admin admin)
        {
            AdminRepository repo = new AdminRepository();
            AdminValidation validation = new AdminValidation(admin);
            if (validation.IsValid)
            {
                Admin existingAdmin = GetAdmin(admin.Username);
                if (existingAdmin != null)
                {
                    repo.EditAdmin(Mapper.Map<ADMIN>(admin));
                }
                else
                {
                    validation.DoesNotExistOnServer(nameof(admin.Username));
                    return validation;
                }
            }
            return validation;
        }
    }
}