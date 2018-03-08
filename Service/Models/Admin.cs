using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Repository.Support;
using Repository;
using AutoMapper;
using Service.Managers;
using Service.Tools;


namespace Service.Models
{
    public enum Rank
    {
        // User,may change admin to 1 and have user as rank 0
        Admin = 0,
        SuperAdmin
    }

    public class Admin
    {
        public string Username { get; set; }

        public string Password { get; set;}

        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        public Rank PermissionLevel { get; set; }

    }
}