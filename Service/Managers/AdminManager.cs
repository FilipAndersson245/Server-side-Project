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

namespace Service.Managers
{
    public class AdminManager
    {
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
