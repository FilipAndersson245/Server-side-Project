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

public enum Rank
{
    Admin,
    SuperAdmin
}

namespace Service.Models
{
    public class Admin
    {
        [Required(ErrorMessage = "Please Provide Username", AllowEmptyStrings = false)]
        [DataType(DataType.Text)]
        [Key]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Provide Password", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{7,15}$", ErrorMessage = "Incorrect password")]
        public string Password { get; set;}

        [StringLength(64)]
        public string PasswordHash { get; set; }

        [StringLength(64)]
        public string Salt { get; set; }


        [Required]
        public Rank PermissionLevel { get; set; }

    }
}