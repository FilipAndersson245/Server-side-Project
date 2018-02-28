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
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Key]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{4,25}$", ErrorMessage = "Password does not match the requirements")]
        public string Password { get; set;}

        [StringLength(64)]
        public string PasswordHash { get; set; }

        [StringLength(64)]
        public string Salt { get; set; }


        [Required]
        public Rank PermissionLevel { get; set; }

    }
}