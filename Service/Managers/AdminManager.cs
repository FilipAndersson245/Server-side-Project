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
        public static Admin getAdmin(string username)
        {
            return Mapper.Map<Admin>(AdminRepository.getAdmin(username));
        }

        public static bool createAdmin(Admin admin)
        {
            return AdminRepository.createAdmin(Mapper.Map<ADMIN>(admin));
        }

        public static List<Admin> getAllAdmins()
        {
            return Mapper.Map<List<ADMIN>, List<Admin>>(AdminRepository.getAllAdmins());
        }
    }
}
