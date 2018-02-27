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
        public Admin GetAdmin(string username)
        {
            AdminRepository repo = new AdminRepository();
            return Mapper.Map<Admin>(repo.GetAdmin(username));
        }

        public bool Login(ModelStateDictionary modelState,string username, string password)
        {
            if (modelState.IsValid)
            {
                AdminRepository repo = new AdminRepository();
                if (!repo.DoesAdminExist(username))
                {
                    modelState.AddModelError("Username", "User does not exist");
                }
                else
                {
                    var dbAdmin = GetAdmin(username);
                    Hashing pwdHash = new Hashing(password, dbAdmin.Salt);
                    repo.GetAdmin(username);
                    if (pwdHash.Equals(dbAdmin.PasswordHash))
                        return true;
                    else
                    {
                        modelState.AddModelError("Password", "Wrong Password!");
                    }
                }
            }
            return false;
        }

        public bool SignUp(ModelStateDictionary modelState, Admin admin)
        {
            if (modelState.IsValid)
            {
                AdminRepository repo = new AdminRepository();
                if (repo.DoesAdminExist(admin.Username))
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
