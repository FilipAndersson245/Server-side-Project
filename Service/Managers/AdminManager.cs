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
        public static Admin GetAdmin(string username)
        {
            return Mapper.Map<Admin>(AdminRepository.GetAdmin(username));
        }

        public static bool CreateAdmin(Admin admin)
        {
            return AdminRepository.CreateAdmin(Mapper.Map<ADMIN>(admin));
        }

        public static List<Admin> GetAllAdmins()
        {
            return Mapper.Map<List<ADMIN>, List<Admin>>(AdminRepository.GetAllAdmins());
        }
    }
}
