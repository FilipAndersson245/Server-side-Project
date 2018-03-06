using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Repository.Support;
using Repository;
using AutoMapper;
using Service.Models;
using Service.Tools;
using System.Web.Mvc;

namespace Service.Managers
{
    public class AdminManager
    {
        public ValidationModel Login(Admin admin)
        {
            ValidationModel validation = new ValidationModel(admin);
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

        public ValidationModel SignUp(Admin admin)
        {
            ValidationModel validation = new ValidationModel(admin);
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
            return repo.CreateAdmin(Mapper.Map<ADMIN>(admin));
        }

        public List<Admin> GetAllAdmins()
        {
            AdminRepository repo = new AdminRepository();
            return Mapper.Map<List<ADMIN>, List<Admin>>(repo.GetAllAdmins());
        }
    }
}
