using AutoMapper;
using Repository;
using Repository.Support;
using Service.Models;
using Service.Tools;
using Service.Validations;
using System;
using System.Collections.Generic;

namespace Service.Managers
{
    public class AdminManager
    {
        private AdminRepository _Repo { get; } = new AdminRepository();

        public Tuple<Admin, AdminValidation> Login(Admin admin)
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
                        return new Tuple<Admin, AdminValidation>(dbAdmin, validation);
                    else
                    {
                        validation.WrongPassword(nameof(admin.Password));
                    }
                }
            }
            return new Tuple<Admin, AdminValidation>(null, validation);
        }

        public Rank getPermissionLevel(string username)
        {
            return (Rank)_Repo.GetPermissionLevel(username);
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
                }
            }
            return validation;
        }

        public Admin GetAdmin(string username)
        {
            return Mapper.Map<Admin>(_Repo.GetAdmin(username));
        }

        public bool CreateAdmin(Admin admin)
        {
            return _Repo.CreateAdmin(Mapper.Map<ADMIN>(admin));
        }

        public List<Admin> GetAllAdmins()
        {
            return Mapper.Map<List<ADMIN>, List<Admin>>(_Repo.GetAllAdmins());
        }

        public bool DeleteAdmin(string id)
        {
            return _Repo.DeleteAdmin(id);
        }

        public AdminValidation EditAdmin(Admin admin, bool PasswordNotEdited = false)
        {
            AdminValidation validation = new AdminValidation(admin, PasswordNotEdited);
            if (validation.IsValid)
            {
                Admin existingAdmin = GetAdmin(admin.Username);
                if (existingAdmin != null)
                {
                    _Repo.EditAdmin(Mapper.Map<ADMIN>(admin));
                }
                else
                {
                    validation.DoesNotExistOnServer(nameof(admin.Username));
                }
            }
            return validation;
        }
    }
}