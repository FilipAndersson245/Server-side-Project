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
        public bool Login(ModelStateDictionary modelState, Admin admin)
        {
            ValidationModel validation = new ValidationModel(admin);
            var dict = validation.ErrorDict;
            var IsValid = validation.IsValid;
            if (IsValid)
            {
                Admin dbAdmin = GetAdmin(admin.Username);
                if (dbAdmin == null)
                {
                    validation.DoesNotExistOnServer(nameof(admin.Username));
                    modelState.AddModelError("Username", "User does not exist!");
                }
                else
                {
                    Hashing pwdHash = new Hashing(admin.Password, dbAdmin.Salt);
                    if (pwdHash.Equals(dbAdmin.PasswordHash))
                        return true;
                    else
                    {
                        validation.WrongPassword(nameof(admin.Username));
                        modelState.AddModelError("Password", "Wrong Password!");
                    }
                }
            }
            return false;
        }

        public bool Login(List<KeyValuePair<string, ModelState>> modelList, string username, string password)
        {
            ModelState a = new ModelState();
            
            throw new NotImplementedException();
        }

        public bool SignUp(ModelStateDictionary modelState, Admin admin)
        {
            if (modelState.IsValid)
            {
                Admin existingAdmin = GetAdmin(admin.Username);
                if (existingAdmin != null)
                    modelState.AddModelError("Username", "Username already in use!");
                else
                {
                    Hashing hash = new Hashing(admin.Password);
                    admin.Password = null;
                    admin.PasswordHash = hash.Hash;
                    admin.Salt = hash.Salt;
                    CreateAdmin(admin);
                    return true;
                }
            }
            return false;
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
